using BlindW.Controllers.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BlindW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetTextController : ControllerBase
    {
        private static readonly Dictionary<string, string[]> _wordCache = new();
        private static readonly object _lock = new();

        private static readonly string[] PunctuationMarks = { ".", ",", "!", "?", ";", ":" };
        private static readonly string[] Numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

        [HttpPost("randomText")]
        public async Task<ActionResult<IEnumerable<string>>> GetRandomText([FromBody] GetTextRequest request)
        {
            if (request.WordCount <= 0)
                return BadRequest("Количество слов должно быть больше 0.");

            if (request.WordCount > 500)
                return BadRequest("Слишком большое количество слов. Максимум: 500.");

            if (string.IsNullOrWhiteSpace(request.LanguageCode))
                return BadRequest("LanguageCode обязателен.");

            var languageCode = request.LanguageCode.Trim().ToLower();

            if (languageCode != "en" && languageCode != "ru")
                return BadRequest("Поддерживаются только языки: en, ru.");

            var words = await LoadWords(languageCode);

            if (words.Length == 0)
                return Problem("Словарь пуст.");

            var result = Enumerable.Range(0, request.WordCount)
                .Select(index => GenerateToken(words, request.IsNumbersEnabled, request.IsPunctuationEnabled, index))
                .ToList();

            return Ok(result);
        }

        private async Task<string[]> LoadWords(string languageCode)
        {
            if (_wordCache.TryGetValue(languageCode, out var cachedWords) && cachedWords.Length > 0)
                return cachedWords;

            var fileName = languageCode == "ru" ? "words-ru.txt" : "words-en.txt";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Data", fileName);

            if (!System.IO.File.Exists(path))
                return Array.Empty<string>();

            var loadedWords = await System.IO.File.ReadAllLinesAsync(path);

            loadedWords = loadedWords
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .Select(w => w.Trim())
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToArray();

            lock (_lock)
            {
                if (!_wordCache.ContainsKey(languageCode))
                    _wordCache[languageCode] = loadedWords;
            }

            return loadedWords;
        }

        private static string GenerateToken(
            string[] words,
            bool isNumbersEnabled,
            bool isPunctuationEnabled,
            int index)
        {
            var random = Random.Shared;

            string token;

            var useNumber = isNumbersEnabled && random.Next(0, 10) == 0; // ~10% шанс
            if (useNumber)
            {
                token = Numbers[random.Next(Numbers.Length)];
            }
            else
            {
                token = words[random.Next(words.Length)];
            }

            var usePunctuation = isPunctuationEnabled && index > 0 && random.Next(0, 6) == 0; // ~16% шанс
            if (usePunctuation)
            {
                token += PunctuationMarks[random.Next(PunctuationMarks.Length)];
            }

            return token;
        }
    }
}
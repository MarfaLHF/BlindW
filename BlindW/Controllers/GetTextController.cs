using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BlindW.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetTextController : ControllerBase
    {
        private static string[]? _words;

        [HttpGet("randomText")]
        public async Task<string> GetRandomText(int wordCount)
        {
            // Загружаем файл один раз в память (кешируем)
            if (_words == null || _words.Length == 0)
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "Data", "words.txt");

                if (!System.IO.File.Exists(path))
                {
                    return $"Файл не найден: {path}";
                }

                _words = await System.IO.File.ReadAllLinesAsync(path);
            }

            // Генерация случайного текста
            var rnd = new Random();
            var result = Enumerable.Range(0, wordCount)
                                   .Select(_ => _words[rnd.Next(_words.Length)])
                                   .ToList();

            // Возвращаем JSON-массив слов
            return JsonSerializer.Serialize(result);
        }
    }
}

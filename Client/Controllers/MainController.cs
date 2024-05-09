using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class MainController : Controller
    {
        private readonly InterfaceClient _apiService;

        public MainController(InterfaceClient apiService)
        {
            _apiService = apiService;
        }
        public async Task<ActionResult> Index()
        {
            string Text = "qwerty qwerty";
            TextViewModel textViewModel = await CountWordsAndCharacters(Text);

            return View(textViewModel);
        }
        
        public ActionResult Result()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Result(double totalTime, int countCharacters, int correctSymbols, double accuracy)
        {
            // Получаем данные из localStorage
            string userId = "d3e12834-f4d6-4e20-9df1-e6884b7284fb"; // Замените на ваш UserId
            int testSettingId = 0; // Замените на ваш TestSettingId
            double wpm = (countCharacters / 5) / (totalTime / 60);

            // Создаем новый объект Result
            Result result = new Result
            {
                UserId = userId,
                TestSettingId = testSettingId,
                CountCharacters = 1,
                TotalTime = 1,
                Wpm = 1,
                Accuracy = 1
            };

            // Сохраняем результат в базе данных
            _apiService.PostTestResult(result);

            return View(result);
        }

        public async Task<TextViewModel> CountWordsAndCharacters(string text)
        {
            return await Task.Run(() =>
            {
                int wordCount = 0, charCount = 0;

                text = text.Trim();

                if (!string.IsNullOrEmpty(text))
                {
                    wordCount = text.Split(null).Length;
                    charCount = text.Length;
                }
                TextViewModel textViewModel = new TextViewModel
                {
                    Text = text,
                    WordCount = wordCount,
                    CharCount = charCount
                };
                return textViewModel;
            });
        }
    }
}

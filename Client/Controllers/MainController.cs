using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Client.Controllers
{
    public class MainController : Controller
    {
        private readonly InterfaceClient _apiService;

        public MainController(InterfaceClient apiService)
        {
            _apiService = apiService;
        }
        [Authorize]
        public async Task<ActionResult> Index(int wordCount = 25)
        {
            string jsonResponse = await _apiService.GetRandomText(wordCount); // Передайте wordCount в GetRandomText
            List<string> words = JsonConvert.DeserializeObject<List<string>>(jsonResponse);
            string Text = string.Join(" ", words);
            TextViewModel textViewModel = await CountWordsAndCharacters(Text);

            return View(textViewModel);
        }

        [Authorize]
        public ActionResult Result()
        {
            return View();
        }  

        [HttpPost]
        public async Task<ActionResult> Result(double totalTime, int countCharacters, int correctSymbols)
        {
            var user = await _apiService.GetUserByEmail(User.FindFirst(ClaimTypes.Name).Value);

            int testSettingId = 0; // Заменить на ваш TestSettingId
            double wpm = Math.Round((countCharacters / 5.0) / (totalTime / 60.0));
            double accuracy = Math.Round((double)correctSymbols / countCharacters * 100);
            totalTime = Math.Round(totalTime / 1000);

            Result result = new Result
            {
                UserId = user.Id,
                TestSettingId = testSettingId,
                CountCharacters = countCharacters,
                TotalTime = totalTime,
                Wpm = wpm,
                Accuracy = accuracy
            };

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

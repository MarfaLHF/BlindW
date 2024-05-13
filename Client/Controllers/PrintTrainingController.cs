using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace Client.Controllers
{
    public class PrintTrainingController : Controller
    {
        private readonly InterfaceClient _apiService;

        public PrintTrainingController(InterfaceClient apiService)
        {
            _apiService = apiService;
        }
        public async Task<ActionResult> Index()
        {
            string Text = "On your first visit to the site you will be met with an exciting test a test of printing skills after successful completion of the test you will see your result which will be compared with the data of other users";

            TextViewModel textViewModel = await CountWordsAndCharacters(Text);

            return View(textViewModel);
        }
        public ActionResult Result()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Result(double totalTime, int countCharacters, int correctSymbols)
        {

            int testSettingId = 0; // Заменить на ваш TestSettingId
            double wpm = Math.Round((countCharacters / 5.0) / (totalTime / 60.0));
            double accuracy = Math.Round((double)correctSymbols / countCharacters * 100);
            totalTime = Math.Round(totalTime / 1000);

            Result result = new Result
            {
                UserId = "1",
                TestSettingId = testSettingId,
                CountCharacters = countCharacters,
                TotalTime = totalTime,
                Wpm = wpm,
                Accuracy = accuracy
            };



            return View("Result", result);
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

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
        [Authorize] 
        public async Task<ActionResult> Index()
        {
            string Text = "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqQQQQQQQQQQQQQ";
            TextViewModel textViewModel = await CountWordsAndCharacters(Text);
            
            return View(textViewModel);
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
        private double CalculateWpm(string text, int wordCount, double minutes)
        {
            double averageWordLength = 7.2;
            double adjustedCharCount = text.Length - (wordCount * (averageWordLength - 1));

            double wpm = (adjustedCharCount / wordCount) * 60 / minutes;
            return Math.Round(wpm, 2);
        }
        private double CalculateAccuracy(string userInput, string referenceText)
        {
            int matchedCharacters = 0;
            int minLength = Math.Min(userInput.Length, referenceText.Length);

            for (int i = 0; i < minLength; i++)
            {
                if (userInput[i] == referenceText[i])
                {
                    matchedCharacters++;
                }
            }

            double accuracy = (matchedCharacters / (double)minLength) * 100;
            return Math.Round(accuracy, 2); 
        }
        public ActionResult Result()
        {
            ViewBag.UserInput = TempData["UserInput"];
            ViewBag.InputTime = TempData["InputTime"];
            return View();
        }
    }
}

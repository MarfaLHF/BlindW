using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
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
            string jsonResponse = await _apiService.GetRandomText(15);
            List<string> words = JsonConvert.DeserializeObject<List<string>>(jsonResponse);
            string Text = string.Join(" ", words);
            TextViewModel textViewModel = await CountWordsAndCharacters(Text);

            return View(textViewModel);
        }

        public ActionResult Result()
        {
            if (TempData["Result"] != null)
            {
                var result = JsonConvert.DeserializeObject<Result>(TempData["Result"].ToString());
                return View(result);
            }

            return View(new Result()); // Возвращаем пустую модель, чтобы избежать null reference
        }

        [HttpPost]
        public async Task<ActionResult> Result(double totalTime, int countCharacters, int correctSymbols)
        {
            var user = await _apiService.GetUserByEmail(User.FindFirst(ClaimTypes.Name).Value);

            int testSettingId = 0;

            double wpm = (totalTime > 0)
                ? Math.Round((countCharacters / 5.0) / (totalTime / 60.0))
                : 0;

            double accuracy = (countCharacters > 0)
                ? Math.Round((double)correctSymbols / countCharacters * 100)
                : 0;

            totalTime = Math.Round(totalTime / 1000); // перевод в секунды

            Result result = new Result
            {
                UserId = user.Id,
                TestSettingId = testSettingId,
                CountCharacters = countCharacters,
                TotalTime = totalTime,
                Wpm = wpm,
                Accuracy = accuracy
            };

            await _apiService.PostTestResult(result);

            TempData["Result"] = JsonConvert.SerializeObject(result);

            return RedirectToAction("Result");
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

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}

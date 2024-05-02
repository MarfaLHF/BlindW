using Client.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Client.Controllers
{
    public class PrintTrainingController : Controller
    {
        public async Task<ActionResult> Index()
        {
            string Text = "qqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqqq";
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

        public ActionResult Test()
        {
            return View();
        }
            [HttpPost]
        public ActionResult Test(string userInput)
        {
            // Сохраняем введенный текст и время ввода в TempData
            TempData["UserInput"] = userInput;
            TempData["InputTime"] = DateTime.Now;
            // Перенаправляем на другую страницу
            return RedirectToAction("Result");
        }

        public ActionResult Result()
        {
            return View();
        }

    }
}

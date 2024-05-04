using Client.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Client.Controllers
{
    public class PrintTrainingController : Controller
    {
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

        public ActionResult Test()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Test(string userInput)
        {
            TempData["UserInput"] = userInput;
            TempData["InputTime"] = DateTime.Now;
            return RedirectToAction("Result");
        }

        public ActionResult Result()
        {
            ViewBag.UserInput = TempData["UserInput"];
            ViewBag.InputTime = TempData["InputTime"];
            return View();
        }

    }
}

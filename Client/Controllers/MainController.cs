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

using Client.Models;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly InterfaceClient _userApiClient;

        public HomeController(ILogger<HomeController> logger, InterfaceClient userApiClient)
        {
            _logger = logger;
            _userApiClient = userApiClient;
        }

        public async Task<IActionResult> Index() 
        {
            //Пример получение записей
            //var lessons = await _userApiClient.GetLessons();
            //return View(lessons);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userApiClient.RegisterUser(model);

            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error);
            }

            return View(model);
        }

    }
}

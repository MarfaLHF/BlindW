using Client.Models.Requests;
using Client.Services;
using Microsoft.AspNetCore.Mvc;
using Refit;

namespace Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly InterfaceClient _authService;
        public AccountController(InterfaceClient authService)
        {
            _authService = authService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var response = await _authService.Login(model);

                // Сохраните токены доступа и обновления в безопасном месте (например, в куки или в сессии)

                return RedirectToAction("Index", "Home");
            }
            catch (ApiException ex)
            {
                // Обработайте ошибки, возникшие при попытке входа в систему
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }
    }
}


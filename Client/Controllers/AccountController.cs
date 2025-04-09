using BlindW.Data.Models;
using Client.Models.Requests;
using Client.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Security.Claims;
using Client.Models;

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


                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim("AccessToken", response.AccessToken),
                new Claim("RefreshToken", response.RefreshToken),
            };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties();

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Main");
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                ModelState.AddModelError("CustomError", "Пользователь не найден. Пожалуйста, проверьте правильность введенных данных.");
                return View(model);
            }
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistrationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                await _authService.Register(model);
                TempData["RegistrationMessage"] = "Registration successful! Please check your email for confirmation.";
                return RedirectToAction("Login");
            }
            catch (ApiException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(model);
            }
        }



        public async Task<IActionResult> Profile(string sortOrder)
        {
            var user = await _authService.GetUserByEmail(User.FindFirst(ClaimTypes.Name).Value);

            IEnumerable<TestResult> testResults = new List<TestResult>();

            try
            {
                testResults = await _authService.GetUserTestResults(user.Id);
            }
            catch (ValidationApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    // Просто нет результатов — оставляем testResults пустым
                    testResults = new List<TestResult>();
                }
                else
                {
                    throw;
                }
            }

            // Сортировка
            switch (sortOrder)
            {
                case "date_desc":
                    testResults = testResults.OrderByDescending(r => r.TestDateTime).ToList();
                    break;
                default:
                    testResults = testResults.OrderBy(r => r.TestDateTime).ToList();
                    break;
            }

            var viewModel = new UserProfileViewModel
            {
                User = user,
                TestResults = testResults
            };

            return View(viewModel);
        }



        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }


    }
}
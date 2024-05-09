using BlindW.Data.Models;
using Client.Models.Requests;
using Client.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System.Security.Claims;

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
                new Claim("RefreshToken", response.RefreshToken)
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
    }
}
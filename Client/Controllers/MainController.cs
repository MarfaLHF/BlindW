using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class MainController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }
    }
}

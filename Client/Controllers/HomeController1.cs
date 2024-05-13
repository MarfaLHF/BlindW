using Client.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    public class HomeController1 : Controller
    {
        // GET: HomeController1
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(double totalTime, int countCharacters, int correctSymbols)
        {

            int testSettingId = 0; // Заменить на ваш TestSettingId
            double wpm = Math.Round((countCharacters / 5.0) / (totalTime / 60.0));
            double accuracy = Math.Round((double)correctSymbols / countCharacters * 100);
            totalTime = Math.Round(totalTime / 1000);

            var resulttt = new Result
            {
                UserId = "1",
                TestSettingId = 1,
                CountCharacters = 1,
                TotalTime = 1,
                Wpm = 1,
                Accuracy = 1
            };

            ViewBag.Result = resulttt;

            return View();
        }
        // GET: HomeController1/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;
using System.Diagnostics;

namespace MvcMovie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult MasterDetail()
        {
            return View();
        }

        public IActionResult Tree()
        {
            return View();
        }

        public IActionResult Refresh()
        {
            return View();
        }

        public IActionResult TreeBox()
        {
            return View();
        }

        public IActionResult Dictionaries()
        {
            return View();
        }

        public IActionResult Organization()
        {
            return View();
        }

        public IActionResult ImportAssetsFromExcel()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
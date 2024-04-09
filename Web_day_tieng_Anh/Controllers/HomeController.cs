using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Controllers
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
        public IActionResult about() 
        {
            return View();
        }
        public IActionResult Courses() 
        {
            return View();
        }
        public IActionResult Trainers()
        {
            return View();
        }
        //Dua courses va detaild vao 1 file controllel
        public IActionResult CoursesDetail()
        {
            return View();
        }
        public IActionResult Event()
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

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Controllers
{
    public class HomeController : Controller
    {

        private readonly ICoursesRepository _coursesRepository;

        public HomeController(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
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
        //public IActionResult Courses() 
        //{
        //    return View();
        //}
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
        public IActionResult Class10()
        {
            return View();
        }
            
        public async Task<IActionResult> Courses()
        {
            var courses = await _coursesRepository.GetAllAsync();
            return View(courses);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

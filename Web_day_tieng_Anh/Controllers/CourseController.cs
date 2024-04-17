using Microsoft.AspNetCore.Mvc;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;

        public CourseController(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var courses = await _coursesRepository.GetAllAsync();
            return View(courses);
        }

        // Hiển thị form thêm danh mục mới
        public IActionResult Add()
        {
            return View();
        }

        // Xử lý thêm danh mục mới
        [HttpPost]
        public async Task<IActionResult> Add(Course course)
        {
            if (ModelState.IsValid)
            {
                await _coursesRepository.AddAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
    }
}

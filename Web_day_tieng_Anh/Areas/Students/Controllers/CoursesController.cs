using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Areas.Students.Controllers
{
    [Area("Students")]
    [Authorize(Roles = "Student")]
    public class CoursesController : Controller
    {
        
        private readonly ICoursesRepository _coursesRepository;
        

        public CoursesController(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var courses = await _coursesRepository.GetAllAsync();
            return View(courses);
        }

        // Hiển thị thông tin chi tiết
        public async Task<IActionResult> Display(int id)
        {
            var course = await _coursesRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

    }
}

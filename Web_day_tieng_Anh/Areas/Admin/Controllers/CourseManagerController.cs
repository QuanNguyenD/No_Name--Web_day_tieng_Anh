using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CourseManagerController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;

        public CourseManagerController(ICoursesRepository coursesRepository)
        {
            _coursesRepository = coursesRepository;
        }

        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var courses = await _coursesRepository.GetAllAsync();
            return View(courses);
        }
    }
}

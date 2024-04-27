using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Areas.Students.Controllers
{
    [Area("Students")]
    [Authorize(Roles = "Student")]
    public class LessonsController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ILessonRepository _lessonRepository;

        public LessonsController(ICoursesRepository coursesRepository,ILessonRepository lessonRepository)
        {
            _coursesRepository = coursesRepository;
            _lessonRepository = lessonRepository;
           
        }

        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var courses = await _lessonRepository.GetAllAsync();
            return View(courses);
        }

        // Hiển thị thông tin chi tiết
        public async Task<IActionResult> Display(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }
    }
    
}

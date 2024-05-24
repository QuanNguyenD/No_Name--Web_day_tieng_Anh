using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Data;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Areas.Students.Controllers
{
    [Area("Students")]
    [Authorize(Roles = "Student")]
    public class LessonsController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly ApplicationDbContext _context;

        public LessonsController(ICoursesRepository coursesRepository, ILessonRepository lessonRepository, ApplicationDbContext context)
        {
            _coursesRepository = coursesRepository;
            _lessonRepository = lessonRepository;
            _context = context;

        }

        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index(int courseId)
        {

            var lessons = _context.Lessons.Where(l => l.CourseId == courseId).Include(p => p.Course).ToList();


            if (lessons == null)
            {
                lessons = new List<Lesson>();
            }
            var viewmodel = new Course
            {
                Lessons = lessons,
            };

            return View(lessons);
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

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Areas.Students.Controllers
{
    [Area("Students")]
    [Authorize(Roles = "Student")]
    public class QuestionsController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(ILessonRepository lessonRepository, ICoursesRepository coursesRepository, IQuestionRepository questionRepository)
        {
            _lessonRepository = lessonRepository;
            _coursesRepository = coursesRepository;
            _questionRepository = questionRepository;
        }

        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var courses = await _questionRepository.GetAllAsync();
            return View(courses);
        }

        // Hiển thị thông tin chi tiết
        public async Task<IActionResult> Display(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }
    }
}

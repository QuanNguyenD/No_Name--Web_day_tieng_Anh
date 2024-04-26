using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Areas.Students.Controllers
{
    [Area("Students")]
    [Authorize(Roles = "Student")]
    public class AnswersController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IAnswerRepository _answerRepository;
        private readonly IQuestionRepository _questionRepository;

        public AnswersController(ILessonRepository lessonRepository, ICoursesRepository coursesRepository, IAnswerRepository answerRepository, IQuestionRepository questionRepository)
        {
            _lessonRepository = lessonRepository;
            _coursesRepository = coursesRepository;
            _answerRepository = answerRepository;
            _questionRepository = questionRepository;
        }

        // Hiển thị danh sách danh mục
        //public async Task<IActionResult> Index()
        //{
        //    var courses = await _lessonRepository.GetAllAsync();
        //    return View(courses);
        //}

        // Hiển thị thông tin chi tiết
        public async Task<IActionResult> Display(int id)
        {
            var answer = await _answerRepository.GetByIdAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            return View(answer);
        }
        public async Task<IActionResult> Add()
        {
            var questions = await _answerRepository.GetAllAsync();
            //ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Questions = new SelectList(questions, "QuetionId", "QuestionContent");    
            return View();
        }


        // Xử lý thêm cay tra loi 
        [HttpPost]
        public async Task<IActionResult> Add(Answer answer)
        {
            if (ModelState.IsValid)
            {
                
                await _answerRepository.AddAsync(answer);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var questions = await _questionRepository.GetAllAsync();
            ViewBag.Questions = new SelectList(questions, "QuetionId", "QuestionContent");
            return View(answer);
        }
        public async Task<IActionResult> Update(int id)
        {
            var answer = await _answerRepository.GetByIdAsync(id);
            if (answer == null)
            {
                return NotFound();
            }
            var questions = await _questionRepository.GetAllAsync();
            ViewBag.Questions = new SelectList(questions, "QuetionId", "QuestionContent",answer.QuestionId );
            return View(answer);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Answer answer)
        {
            
            if (id !=answer.AnswerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingAnswer = await _answerRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync


                
                existingAnswer.AnswerContent = answer.AnswerContent;


                await _answerRepository.UpdateAsync(existingAnswer);
                return RedirectToAction(nameof(Index));
            }
            var categories = await _questionRepository.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(answer);
        }


        
    }
}

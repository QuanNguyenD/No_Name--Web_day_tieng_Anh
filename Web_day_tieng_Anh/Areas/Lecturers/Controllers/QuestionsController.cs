using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Frozen;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace Web_day_tieng_Anh.Areas.Lecturers.Controllers
{
    [Area("Lecturers")]
    [Authorize(Roles = "Lecturers")]
    public class QuestionsController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly ITestRepository _testRepository;
        private readonly IQuestionRepository _questionRepository;

        public QuestionsController(ICoursesRepository coursesRepository, ILessonRepository lessonRepository, ITestRepository testRepository, IQuestionRepository questionRepository)
        {
            _coursesRepository = coursesRepository;
            _lessonRepository = lessonRepository;
            _testRepository = testRepository;
            _questionRepository = questionRepository;
        }

        public async Task<IActionResult> Index()
        {
            var questions = await _questionRepository.GetAllAsync();
            return View(questions);
        }
        public async Task<IActionResult> Display(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }
        public async Task<IActionResult> Add()
        {
            var test = await _testRepository.GetAllAsync();
            ViewBag.Test = new SelectList(test, "TestId", "TestName");

            return View();
        }


        // Xử lý thêm  mới
        [HttpPost]
        public async Task<IActionResult> Add(Question question)
        {
            if (ModelState.IsValid)
            {


                await _questionRepository.AddAsync(question);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var test = await _testRepository.GetAllAsync();
            ViewBag.Test = new SelectList(test, "TestId", "TestName", question.TestId);
            return View(test);
        }
        public async Task<IActionResult> Update(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            var test = await _testRepository.GetAllAsync();
            ViewBag.Test = new SelectList(test, "TestId", "TestName");
            return View(test);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Question question)
        {

            if (id != question.TestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingQuestion = await _questionRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync




                // Cập nhật các thông tin khác 

                existingQuestion.QuestionContent = question.QuestionContent;
                



                await _questionRepository.UpdateAsync(existingQuestion);
                return RedirectToAction(nameof(Index));
            }
            var test = await _testRepository.GetAllAsync();
            ViewBag.Test = new SelectList(test, "CourseId", "CourseName", question.TestId);
            return View(question);
        }


        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }


        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _questionRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

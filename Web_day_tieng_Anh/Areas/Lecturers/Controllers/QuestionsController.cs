using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Data;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Repository;

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
        private readonly ApplicationDbContext _context;

        public QuestionsController(ICoursesRepository coursesRepository, ILessonRepository lessonRepository, ITestRepository testRepository, IQuestionRepository questionRepository, ApplicationDbContext context)
        {
            _coursesRepository = coursesRepository;
            _lessonRepository = lessonRepository;
            _testRepository = testRepository;
            _questionRepository = questionRepository;
            _context = context;
        }

        public async Task<IActionResult> Index(int testId)
        {
            var questions = await _context.Questions
                .Where(l => l.TestId == testId)
                .Include(p => p.Test)
                .Include(q => q.Answers)  // Include answers
                .ToListAsync();
            if (questions == null)
            {
                questions = new List<Question>();
            }
            return View(questions);
        }

        public async Task<IActionResult> Display(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            // Include cả dữ liệu của các câu trả lời
            await _context.Entry(question)
                .Collection(q => q.Answers)
                .LoadAsync();

            return View(question);
        }


        public async Task<IActionResult> Add(int testId)
        {
            var test = await _testRepository.GetByIdAsync(testId);

            if (test == null)
            {
                return NotFound();
            }

            ViewBag.TestId = testId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Question question)
        {
            if (ModelState.IsValid)
            {
                // Thêm câu hỏi và câu trả lời vào cơ sở dữ liệu
                _context.Questions.Add(question);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index), new { testId = question.TestId });
            }

            ViewBag.TestId = question.TestId;
            return View(question);
        }


        public async Task<IActionResult> Update(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }

            var test = await _testRepository.GetAllAsync();
            ViewBag.Test = new SelectList(test, "TestId", "TestName", question.TestId);
                await _context.Entry(question)
        .Collection(q => q.Answers)
        .LoadAsync();
            return View(question);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] QuestionUpdateModel questionUpdate)
        {
            if (ModelState.IsValid)
            {
                var existingQuestion = await _context.Questions
                    .Include(q => q.Answers)
                    .FirstOrDefaultAsync(q => q.QuestionId == questionUpdate.QuestionId);

                if (existingQuestion == null)
                {
                    return NotFound();
                }

                existingQuestion.QuestionContent = questionUpdate.QuestionContent;
                _context.Answers.RemoveRange(existingQuestion.Answers);

                foreach (var answer in questionUpdate.Answers)
                {
                    existingQuestion.Answers.Add(new Answer
                    {
                        AnswerContent = answer.AnswerContent,
                        IsCorrect = answer.IsCorrect
                    });
                }

                _context.Questions.Update(existingQuestion);
                await _context.SaveChangesAsync();

                return Ok();
            }

            return BadRequest(ModelState);
        }



        // Lớp dùng để đại diện cho dữ liệu được gửi từ trình duyệt
        public class QuestionUpdateModel
        {
            public int QuestionId { get; set; }
            public string QuestionContent { get; set; }
            public List<AnswerUpdateModel> Answers { get; set; }
        }

        public class AnswerUpdateModel
        {
            public string AnswerContent { get; set; }
            public bool IsCorrect { get; set; }
        }



        public async Task<IActionResult> Delete(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);
            if (question == null)
            {
                return NotFound();
            }
            return View(question);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var question = await _questionRepository.GetByIdAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            var testId = question.TestId;
            await _questionRepository.DeleteAsync(id);

            return RedirectToAction("Index", new { testId });
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Data;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Repository;
using static System.Net.Mime.MediaTypeNames;

namespace Web_day_tieng_Anh.Areas.Lecturers.Controllers
{
    [Area("Lecturers")]
    [Authorize(Roles = "Lecturers")]
    public class TestsController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly ITestRepository _testRepository;
        private readonly ApplicationDbContext _context;

        public TestsController(ICoursesRepository coursesRepository, ILessonRepository lessonRepository, ITestRepository testRepository, ApplicationDbContext context)
        {
            _coursesRepository = coursesRepository;
            _lessonRepository = lessonRepository;
            _testRepository = testRepository;
            _context = context;
        }
        public async Task<IActionResult> Index(int courseId)
        {
            //var tests = await _testRepository.GetAllAsync();
            //return View(tests);
            var tests = _context.Tests.Where(l => l.CourseId == courseId).Include(p => p.Course).ToList();
            if (tests == null)
            {
                tests = new List<Test>();
            }
            var viewmodel = new Course
            {
                Tests = tests,
            };

            return View(tests);
        }
        public async Task<IActionResult> Display(int id)
        {
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }
        //public async Task<IActionResult> Add()
        //{
        //    var course = await _coursesRepository.GetAllAsync();
        //    ViewBag.Course = new SelectList(course, "CourseId", "CourseName");

        //    return View();
        //}
        public async Task<IActionResult> Add(int courseId)
        {
            // Get the Course object corresponding to the courseId
            var course = await _coursesRepository.GetByIdAsync(courseId);

            // If the course doesn't exist, return a 404 Not Found response
            if (course == null)
            {
                return NotFound();
            }

            // Set ViewBag.CourseId
            ViewBag.CourseId = courseId;

            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> Add(int courseId)
        //{
        //    // Get the Course object corresponding to the courseId
        //    var course = await _coursesRepository.GetByIdAsync(courseId);

        //    // If the course doesn't exist, return a 404 Not Found response
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }



        //    ViewBag.CourseId = courseId;
        //    return View();
        //}
        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Test test)
        {
            if (ModelState.IsValid)
            {


                await _testRepository.AddAsync(test);
                //return RedirectToAction(nameof(Index));
                var tests = await _context.Tests
            .Where(l => l.CourseId == test.CourseId)
            .ToListAsync();
                //return RedirectToAction(nameof(Index));
                return View("Index", tests);
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var course = await _coursesRepository.GetAllAsync();
            ViewBag.Course = new SelectList(course, "CourseId", "CourseName");
            return View("Add", test);
        }
        
        public async Task<IActionResult> Update(int id)
        {
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            var courses = await _coursesRepository.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName", test.CourseId);
            return View(test);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Test test)
        {
            var testUp = await _testRepository.GetByIdAsync(id);
            var courseId = testUp.CourseId;

            if (id != test.TestId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingTests = await _testRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync




                // Cập nhật các thông tin khác 

                existingTests.TestDescription = test.TestDescription;
                existingTests.TestName = test.TestName;



                await _testRepository.UpdateAsync(existingTests);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Tests", new { courseId });
            }
            var courses = await _coursesRepository.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName");
            //return View(test);
            return RedirectToAction("Index", "Tests", new { courseId });

        }


        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var test = await _testRepository.GetByIdAsync(id);
            if (test == null)
            {
                return NotFound();
            }
            return View(test);
        }


        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var test = await _testRepository.GetByIdAsync(id);
            
            if (test == null)
            {
                return NotFound();
            }

            var courseId = test.CourseId;
            await _testRepository.DeleteAsync(id);

            return RedirectToAction("Index", "Tests", new { courseId });
        }
    }
}

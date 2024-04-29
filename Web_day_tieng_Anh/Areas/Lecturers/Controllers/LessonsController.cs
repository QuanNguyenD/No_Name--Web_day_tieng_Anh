using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Areas.Lecturers.Controllers
{
    [Area("Lecturers")]
    [Authorize(Roles = "Lecturers")]
    public class LessonsController : Controller 
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ILessonRepository _lessonRepository;

        public LessonsController(ICoursesRepository coursesRepository, ILessonRepository lessonRepository)
        {
            _coursesRepository = coursesRepository;
            _lessonRepository = lessonRepository;

        }

        //Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var courses = await _lessonRepository.GetAllAsync();
            return View(courses);
        }
        //public IActionResult Index(int courseId)
        //{
        //    // Retrieve lessons associated with the specified courseId
        //    var lessons = _lessonRepository.Lessons.Where(l => l.CourseId == courseId).ToList();

        //    return View(lessons);
        //}

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
        public async Task<IActionResult> Add()
        {
            var course = await _coursesRepository.GetAllAsync();
            ViewBag.Course = new SelectList(course, "CourseId", "CourseName");
            
            return View();
        }


        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Lesson lesson)
        {
            if (ModelState.IsValid)
            {
                
                
                await _lessonRepository.AddAsync(lesson);
                return RedirectToAction(nameof(Index));
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var course = await _coursesRepository.GetAllAsync();
            ViewBag.Course = new SelectList(course, "CourseId", "CourseName");
            return View(course);
        }
        public async Task<IActionResult> Update(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            var courses = await _coursesRepository.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName", lesson.CourseId);
            return View(lesson);
        }
        // Xử lý cập nhật sản phẩm
        [HttpPost]
        public async Task<IActionResult> Update(int id, Lesson lesson)
        {
            
            if (id != lesson.LessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingLessons = await _lessonRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync


                
                
                // Cập nhật các thông tin khác 
                
                existingLessons.LessonName = lesson.LessonName;
                existingLessons.LessonDescription = lesson.LessonDescription;
                


                await _lessonRepository.UpdateAsync(existingLessons);
                return RedirectToAction(nameof(Index));
            }
            var courses = await _coursesRepository.GetAllAsync();
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName");
            return View(lesson);
        }


        // Hiển thị form xác nhận xóa sản phẩm
        public async Task<IActionResult> Delete(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            return View(lesson);
        }


        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _lessonRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

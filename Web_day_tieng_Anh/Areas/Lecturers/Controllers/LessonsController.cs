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



       

        public async Task<IActionResult> Index(int courseId)
        {
            //var lessons1 = await _lessonRepository.GetAllAsync();
            // Retrieve lessons associated with the specified courseId
            
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
        public async Task<IActionResult> Add()
        {
            
            var course = await _coursesRepository.GetAllAsync();
            ViewBag.Course = new SelectList(course, "CourseId", "CourseName");
            
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Add(int courseId)
        {
            // Get the Course object corresponding to the courseId
            var course = await _coursesRepository.GetByIdAsync(courseId);

            // If the course doesn't exist, return a 404 Not Found response
            if (course == null)
            {
                return NotFound();
            }



            //return View(lessonViewModel);
            ViewData["CourseId"] = courseId;
            return View();
        }
        // Xử lý thêm sản phẩm mới
        [HttpPost]
        public async Task<IActionResult> Add(Lesson lesson, IFormFile UpVideoUrl)
        {

            if (ModelState.IsValid)
            {
                if (UpVideoUrl != null)
                {
                    
                    lesson.ImgUrl = await SaveVideo(UpVideoUrl);
                }

                await _lessonRepository.AddAsync(lesson);
                var lessons = await _context.Lessons
             .Where(l => l.CourseId == lesson.CourseId)
             .ToListAsync();
                //return RedirectToAction(nameof(Index));
                return View("Index", lessons);
            }
            // Nếu ModelState không hợp lệ, hiển thị form với dữ liệu đã nhập
            var course = await _coursesRepository.GetAllAsync();


            ViewBag.Course = new SelectList(course, "CourseId", "CourseName");
            return View(lesson);
        }
        private async Task<string> SaveVideo(IFormFile UpVideoUrl)
        {
            var savePath = Path.Combine("wwwroot/videos", UpVideoUrl.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await UpVideoUrl.CopyToAsync(fileStream);
            }
            return "/videos/" + UpVideoUrl.FileName;
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
        public async Task<IActionResult> Update(int id, Lesson lesson, IFormFile videoUrl)
        {
            ModelState.Remove("ImgUrl");
            var lessonUp = await _lessonRepository.GetByIdAsync(id);
            var courseId = lessonUp.CourseId;
            if (id != lesson.LessionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingLessons = await _lessonRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync


                if (videoUrl == null)
                {
                    lesson.ImgUrl = existingLessons.ImgUrl;
                }
                else
                {
                    // Lưu hình ảnh mới
                    
                    lesson.ImgUrl = await SaveVideo(videoUrl);
                }

                // Cập nhật các thông tin khác 

                existingLessons.LessonName = lesson.LessonName;
                existingLessons.LessonDescription = lesson.LessonDescription;
                


                await _lessonRepository.UpdateAsync(existingLessons);
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "Lessons", new { courseId });
            }
            var courses = await _coursesRepository.GetAllAsync();
            
            ViewBag.Courses = new SelectList(courses, "CourseId", "CourseName");
            //return View(lesson);
            return RedirectToAction("Index", "Lessons", new { courseId });
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


        // Xử lý xóa bài học
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lesson = await _lessonRepository.GetByIdAsync(id);
            if (lesson == null)
            {
                return NotFound();
            }
            var courseId = lesson.CourseId;
            await _lessonRepository.DeleteAsync(id);

            return RedirectToAction("Index", "Lessons", new { courseId });
        }
    }
}

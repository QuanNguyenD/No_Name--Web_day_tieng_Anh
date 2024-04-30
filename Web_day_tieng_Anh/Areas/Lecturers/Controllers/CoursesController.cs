using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Data;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Areas.Lecturers.Controllers
{
    [Area("Lecturers")]
    [Authorize(Roles = "Lecturers")]
    public class CoursesController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;
        private readonly ApplicationDbContext _context;
       
        public CoursesController(ICoursesRepository coursesRepository, ApplicationDbContext context)
        {
            _coursesRepository = coursesRepository;
            _context = context; 
        }

        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var courses = await _coursesRepository.GetAllAsync();
            return View(courses);
        }

        // Hiển thị thông tin chi tiết
        public async Task<IActionResult> Display(int id)
        {
            var course = await _coursesRepository.GetByIdAsync(id);
            

            
            
            if (course == null)
            {
                return NotFound();
            }
            var lessons = _context.Lessons.Where(l => l.CourseId == id).ToList();
            if (lessons == null)
            {
                lessons = new List<Lesson>();
            }
            var viewmodel = new Course
            {
                Lessons = lessons,
            };

            return View(viewmodel);


            //return View(course);
        }
        public IActionResult Add()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> Add(Course course)
        {
            if (ModelState.IsValid)
            {
                await _coursesRepository.AddAsync(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }
        public async Task<IActionResult> Update(int id)
        {
            var course = await _coursesRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Course course)
        {
            
            if (id != course.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingCourse = await _coursesRepository.GetByIdAsync(id); // Giả định có phương thức GetByIdAsync


               
                existingCourse.CourseName = course.CourseName;
                existingCourse.CourseDescription = course.CourseDescription;
                existingCourse.Price = course.Price;
                existingCourse.Ratings = course.Ratings;



                await _coursesRepository.UpdateAsync(existingCourse);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _coursesRepository.GetByIdAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }


        // Xử lý xóa sản phẩm
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _coursesRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

    }
}

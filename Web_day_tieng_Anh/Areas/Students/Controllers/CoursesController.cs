using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Areas.Students.Controllers
{
    [Area("Students")]
    [Authorize(Roles = "Student")]
    public class CoursesController : Controller
    {
        
        private readonly ICoursesRepository _coursesRepository;
        private readonly IEnrollmentDetailRepository _enrollmentDetailRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;


        public CoursesController(
            ICoursesRepository coursesRepository,
            IEnrollmentDetailRepository enrollmentDetailRepository,
            IEnrollmentRepository enrollmentRepository)
        {
            _coursesRepository = coursesRepository;
            _enrollmentDetailRepository = enrollmentDetailRepository;
            _enrollmentRepository = enrollmentRepository;
        }

        // Hiển thị danh sách danh mục
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            var enrollments = await _enrollmentRepository.GetByUserIdAsync(userId);
            var enrollmentIds = enrollments.Select(e => e.EnrollmentsId).ToList();
            var enrollmentDetails = await _enrollmentDetailRepository.GetByEnrollmentIdsAsync(enrollmentIds);
            var courseIds = enrollmentDetails.Select(ed => ed.CourseId).ToList();
            var courses = await _coursesRepository.GetByIdsAsync(courseIds);

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
            return View(course);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web_day_tieng_Anh.Data;
using Web_day_tieng_Anh.Extensions;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Repository;

namespace Web_day_tieng_Anh.Controllers
{
    [Authorize(Roles = "Student")]
    public class CartBuyCourseController : Controller
    {
        private readonly ICoursesRepository _coursesRepository;

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public CartBuyCourseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICoursesRepository coursesRepository)
        {

            _context = context;
            _userManager = userManager;
            _coursesRepository = coursesRepository;

        }
        public IActionResult Checkout()
        {
            return View(new Enrollment());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(Enrollment enrollment)

        {
            var cart =
           HttpContext.Session.GetObjectFromJson<CartBuyCourse>("Cart");
            if (cart == null || !cart.Items.Any())
            {
                // Xử lý giỏ hàng trống...
                return RedirectToAction("Index");
            }
            var user = await _userManager.GetUserAsync(User);


            enrollment.UserId = user.Id;
            enrollment.EnrollmentsDate = DateTime.UtcNow;
            enrollment.EnrollmentDetail = cart.Items.Select(i => new EnrollmentDetail
            {
                CourseId = i.CourseId,
                Price = i.Price

            }).ToList();

            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            HttpContext.Session.Remove("Cart");
            return View("OrderCompleted", enrollment.EnrollmentsId); // Trang xác nhận hoàn 

        }


        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<CartBuyCourse>("Cart") ?? new CartBuyCourse();
            return View(cart);
        }
        // Các actions khác...
        private async Task<Course> GetCourseFromDatabase(int courseId)
        {
            // Truy vấn cơ sở dữ liệu để lấy thông tin sản phẩm
            var course = await _coursesRepository.GetByIdAsync(courseId);
            return course;
        }
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<CartBuyCourse>("Cart");
            if (cart is not null)
            {
                cart.RemoveItem(productId);

                // Lưu lại giỏ hàng vào Session sau khi đã xóa mục
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }
        //
        public async Task<IActionResult> AddToCart(int courseId)
        {
            // Giả sử bạn có phương thức lấy thông tin sản phẩm từ productId
            var course = await GetCourseFromDatabase(courseId);




            var cartItem = new CartItem
            {
                CourseId = courseId,
                NameCourse = course.CourseName,
                Price = course.Price


            };

            // Lấy giỏ hàng hiện tại từ session hoặc tạo mới nếu chưa có
            var cart = HttpContext.Session.GetObjectFromJson<CartBuyCourse>("Cart") ?? new CartBuyCourse();


            cart.AddItem(cartItem);

            // Lưu giỏ hàng cập nhật vào session
            HttpContext.Session.SetObjectAsJson("Cart", cart);

            // Quay trở lại trang danh sách giỏ hàng
            return RedirectToAction("Index");

        }
        //
        public async Task<IActionResult> UpdateQuantityAsync(int courseId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<CartBuyCourse>("Cart") ?? new CartBuyCourse();

            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }
    }
}

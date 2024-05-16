using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
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
        private readonly PaypalConfiguration _paypalConfiguration;
        


        public CartBuyCourseController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ICoursesRepository coursesRepository, PaypalConfiguration paypalConfiguration)
        {

            _context = context;
            _userManager = userManager;
            _coursesRepository = coursesRepository;
            _paypalConfiguration = paypalConfiguration;
        }
        public IActionResult Checkout()
        {
            return View(new Enrollment());
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(Enrollment enrollment)

        {
            var cart = HttpContext.Session.GetObjectFromJson<CartBuyCourse>("Cart");
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
        

        public ActionResult FailureView()
        {
            return View();
        }
        public ActionResult SuccessView()
        {
            return View();
        }
        public async Task<IActionResult> PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = _paypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Query["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = $"{Request.Scheme}://{Request.Host}/CartBuyCourse/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    HttpContext.Session.SetString(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Query["guid"].ToString();
                    var executedPayment = ExecutePayment(apiContext, payerId, HttpContext.Session.GetString(guid));
                    //If executed payment failed then we will show payment failure message to user  
                    if (!executedPayment.state.Equals("approved", StringComparison.CurrentCultureIgnoreCase))
                    {
                        
                        return View("FailureView");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View("FailureView");
            }

            //on successful payment, show success page to user.  
            var enrollment = new Enrollment();
            var cart = HttpContext.Session.GetObjectFromJson<CartBuyCourse>("Cart");
            if (cart == null || !cart.Items.Any())
            {
                // Handle empty cart scenario...
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
            return View("SuccessView");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listcourse = HttpContext.Session.GetObjectFromJson<CartBuyCourse>("Cart");
            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            foreach (var item in listcourse.Items)
            {
                
                itemList.items.Add(new Item()
                {
                    name = item.NameCourse,
                    currency = "USD",
                    price = item.Price.ToString(),
                    quantity = "1",
                    sku = item.CourseId.ToString(),
                });
            }
            //Adding Item Details like name, currency, price etc  
            
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = (listcourse.CalculateTotalCost() * 10 /100).ToString(),
                shipping = "0",
                subtotal = listcourse.CalculateTotalCost().ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = ((listcourse.CalculateTotalCost() * 10 / 100) + listcourse.CalculateTotalCost()).ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(), //Generate an Invoice No    
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
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
        public IActionResult RemoveFromCart(int courseId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<CartBuyCourse>("Cart");
            if (cart is not null)
            {
                cart.RemoveItem(courseId);

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

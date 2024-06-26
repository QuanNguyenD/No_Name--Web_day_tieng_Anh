using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Repository;
using Web_day_tieng_Anh.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));








builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{

})
    .AddDefaultUI().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages();


builder.Services.AddDistributedMemoryCache(); builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); options.Cookie.HttpOnly = true; options.Cookie.IsEssential = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICoursesRepository, EFCourseRepository>();
builder.Services.AddScoped<ILessonRepository, EFLessonRepository>();
builder.Services.AddScoped<ITestRepository,EFTestRepository>();
builder.Services.AddScoped<IQuestionRepository, EFQuestionRepository>();
builder.Services.AddScoped<IAnswerRepository, EFAnswerRepository>();
builder.Services.AddScoped<IEnrollmentRepository, EFEnrollmentRepository>();
builder.Services.AddScoped<IEnrollmentDetailRepository, EFEnrollmentDetailRepository>();
builder.Services.Configure<PayPalSettings>(builder.Configuration.GetSection("PayPal"));
builder.Services.AddSingleton<PaypalConfiguration>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();


app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Courses}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();


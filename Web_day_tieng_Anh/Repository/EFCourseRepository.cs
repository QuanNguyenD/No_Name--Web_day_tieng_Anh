using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Data;
namespace Web_day_tieng_Anh.Repository
{
    public class EFCourseRepository : ICoursesRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.ToArrayAsync();   
        }
        public async Task<Course> GetByIdAsync(int id)
        {
            //return await _context.Courses.FindAsync(id);
            return _context.Courses.FirstOrDefault(c => c.CourseId == id);

        }

        public async Task AddAsync(Course course)
        {
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Course course)
        {
            _context.Courses.Update(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var courses = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(courses);
            await _context.SaveChangesAsync();
        }
    }
}

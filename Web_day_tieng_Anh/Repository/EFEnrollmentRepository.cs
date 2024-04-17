using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public class EFEnrollmentRepository : IEnrollmentRepository
    {
        private readonly ApplicationDbContext _context;
        public EFEnrollmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        

        public async Task<IEnumerable<Enrollment>> GetAllAsync()
        {
            return await _context.Enrollments.ToListAsync();
        }

        public async Task<Enrollment> GetByIdAsync(int id)
        {
            return await _context.Enrollments.FindAsync(id);

        }

        public async Task AddAsync(Enrollment enrollment)
        {
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Enrollment enrollment)
        {
            _context.Enrollments.Update(enrollment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var enrollment = await _context.Enrollments.FindAsync(id);
            _context.Enrollments.Remove(enrollment);
            await _context.SaveChangesAsync();
        }
    }
}

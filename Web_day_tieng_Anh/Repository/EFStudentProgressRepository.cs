using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public class EFStudentProgressRepository : IStudentProgressRepository
    {
        private readonly ApplicationDbContext _context;
        public EFStudentProgressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<StudentProgress>> GetAllAsync()
        {
            return await _context.StudentProgresses.ToListAsync();
        }

        public async Task<StudentProgress> GetByIdAsync(int id)
        {
            return await _context.StudentProgresses.FindAsync(id);

        }
    }
}

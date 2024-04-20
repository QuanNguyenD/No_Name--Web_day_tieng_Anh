using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Data;
namespace Web_day_tieng_Anh.Repository
{
    public class EFTestRepository : ITestRepository
    {
        private readonly ApplicationDbContext _context;

        public EFTestRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Test>> GetAllAsync()
        {
            return await _context.Tests.Include(p => p.Course).ToArrayAsync();
        }
        public async Task<Test> GetByIdAsync(int id)
        {
            return await _context.Tests.Include(p => p.Course).FirstOrDefaultAsync(p => p.TestId == id);

        }

        public async Task AddAsync(Test test)
        {
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Test test)
        {
            _context.Tests.Update(test);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var tests = await _context.Tests.FindAsync(id);
            _context.Tests.Remove(tests);
            await _context.SaveChangesAsync();
        }
    }
}

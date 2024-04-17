using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public class EFAnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _context;

        public EFAnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Answer>> GetAllAsync()
        {
            return await _context.Answers.Include(p => p.Question).ToArrayAsync();
        }
        public async Task<Answer> GetByIdAsync(int id)
        {
            return await _context.Answers.Include(p => p.Question).FirstOrDefaultAsync(p => p.AnswerId == id);

        }

        public async Task AddAsync(Answer answer)
        {
            _context.Answers.Add(answer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Answer answer)
        {
            _context.Answers.Update(answer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var answers = await _context.Answers.FindAsync(id);
            _context.Answers.Remove(answers);
            await _context.SaveChangesAsync();
        }
    }
}

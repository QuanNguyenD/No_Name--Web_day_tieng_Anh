using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public class EFQuestionRepository : IQuestionRepository 
    {
        private readonly ApplicationDbContext _context;

        public EFQuestionRepository(ApplicationDbContext context) 
        {
            _context = context;        
        }

        public async Task<IEnumerable<Question>> GetAllAsync()
        {
            return await _context.Questions.Include(p => p.Test).ToArrayAsync();
        }
        public async Task<Question> GetByIdAsync(int id)
        {
            return await _context.Questions.Include(p => p.Test).FirstOrDefaultAsync(p => p.QuestionId == id);

        }

        public async Task AddAsync(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Question question)
        {
            _context.Questions.Update(question);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var questions = await _context.Questions.FindAsync(id);
            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();
        }
    }
}

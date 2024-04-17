using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public class EFCommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public EFCommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllAsync()
        {
            
            return await _context.Comments.Include(p => p.Lesson).ToListAsync();
        }

        public async Task AddAsync(Comment comment)
        {
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Comment comment)
        {
            _context.Comments.Update(comment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
        }


    }
}

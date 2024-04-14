using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public interface IComment
    {
        Task<IEnumerable<Comment>> GetAllAsync();
        Task<Comment> GetByIdAsync(int id);
        Task AddAsync(Comment comment);
        Task UpdateAsync(Comment comment);
        Task DeleteAsync(int id);
    }
}

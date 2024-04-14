using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public interface IAnswerRepository
    {
        Task<IEnumerable<Answer>> GetAllAsync();
        Task<Answer> GetByIdAsync(int id);
        Task AddAsync(Answer answer);
        Task UpdateAsync(Answer answer);
        Task DeleteAsync(int id);
    }
}

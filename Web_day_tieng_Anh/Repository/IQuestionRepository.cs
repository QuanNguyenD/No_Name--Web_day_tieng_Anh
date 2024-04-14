using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllAsync();
        Task<Question> GetByIdAsync(int id);
        Task AddAsync(Question question);
        Task UpdateAsync(Question question);
        Task DeleteAsync(int id);
    }
}

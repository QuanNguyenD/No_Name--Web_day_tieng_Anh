using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public interface ITestScoreRepository
    {
        Task<IEnumerable<TestScore>> GetAllAsync();
        Task<TestScore> GetByIdAsync(int id);
        Task AddAsync(TestScore testScore);
        Task UpdateAsync(TestScore testScore);
        Task DeleteAsync(int id);
    }
}

using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public interface ITestRepository
    {
        Task<IEnumerable<Test>> GetAllAsync();
        Task<Test> GetByIdAsync(int id);
        Task AddAsync(Test test);
        Task UpdateAsync(Test test);
        Task DeleteAsync(int id);
    }
}

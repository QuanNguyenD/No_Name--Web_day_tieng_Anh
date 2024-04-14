using Web_day_tieng_Anh.Models;
namespace Web_day_tieng_Anh.Repository
{
    public interface IEnrollmantRepository
    {
        Task<IEnumerable<Enrollment>> GetAllAsync();
        Task<Enrollment> GetByIdAsync(int id);
        Task AddAsync(Enrollment enrollment);
        Task UpdateAsync(Enrollment enrollment);
        Task DeleteAsync(int id);
    }
}

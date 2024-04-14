using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public interface ICoursesRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<Course> GetByIdAsync(int id);
        Task AddAsync(Course course);
        Task UpdateAsync(Course course);
        Task DeleteAsync(int id);
    }
}

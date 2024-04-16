using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Repository
{
    public interface ICoursesGroupRepository
    {
        Task<IEnumerable<CourseGroup>> GetAllAsync();
        Task<CourseGroup> GetByIdAsync(int id);
        Task AddAsync(CourseGroup courseGroup);
        Task UpdateAsync(CourseGroup courseGroup);
        Task DeleteAsync(int id);
    }
}
}

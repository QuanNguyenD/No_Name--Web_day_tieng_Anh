using Web_day_tieng_Anh.Models;


namespace Web_day_tieng_Anh.Repository
{
    public interface IStudentProgressRepository
    {
        Task<IEnumerable<StudentProgress>> GetAllAsync();
        Task<StudentProgress> GetByIdAsync(int id);
    }
}

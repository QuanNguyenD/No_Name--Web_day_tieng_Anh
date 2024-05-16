using Microsoft.EntityFrameworkCore;
using Web_day_tieng_Anh.Models;
using Web_day_tieng_Anh.Data;
namespace Web_day_tieng_Anh.Repository
{
    public class EFEnrollmentDetailRepository : IEnrollmentDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public EFEnrollmentDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<EnrollmentDetail>> GetAllAsync()
        {
            return await _context.EnrollmentDetail.ToListAsync();
        }

        public async Task<EnrollmentDetail> GetByIdAsync(int id)
        {
            return await _context.EnrollmentDetail.FindAsync(id);

        }
        public async Task<IEnumerable<EnrollmentDetail>> GetByEnrollmentIdsAsync(IEnumerable<int> enrollmentIds)
        {
            return await _context.EnrollmentDetail.Where(ed => enrollmentIds.Contains(ed.EnrollmentId)).ToListAsync();
        }
    }
}

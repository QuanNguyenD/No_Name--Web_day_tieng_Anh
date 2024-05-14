using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class EnrollmentDetail
    {
        [Key]
        public int ProgressId { get; set; }
        public int CourseId { get; set; }
        public int EnrollmentId { get; set; }

        public decimal Price { get; set; }
        public Enrollment? Enrollments { get; set; }
        public Course? Course { get; set; }

    }
}

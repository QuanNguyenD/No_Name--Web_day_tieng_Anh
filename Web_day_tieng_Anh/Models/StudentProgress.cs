using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class StudentProgress
    {
        [Key]
        public int ProgressId { get; set; }
        public int LessionId { get; set; }
        public int EnrollmentId { get; set; }
        public bool Completed { get; set; }
        public DateTime CompletedDate { get; set; }
        public Enrollment? Enrollments { get; set; }
        public Lesson? Lesson { get; set; }

    }
}

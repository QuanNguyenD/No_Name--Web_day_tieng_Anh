using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class StudentProgress
    {
        [Key]
        public int progressId { get; set; }
        public int lessionId { get; set; }
        public int enrollmentId { get; set; }
        public bool completed { get; set; }
        public DateTime completedDate { get; set; }
        public Enrollment? enrollments { get; set; }
        public Lesson? Lesson { get; set; }

    }
}

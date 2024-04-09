using System.ComponentModel.DataAnnotations;
namespace Web_day_tieng_Anh.Models
{
    public class Course
    {
        public int courseId { get; set; }
        [Required, StringLength(100)]
        public string courseName { get; set; }
        [Range(0.01, 10000.00)]
        public string courseDescription { get; set; }

        public int level { get; set; } 
        public decimal price { get; set; }   

        public int ratings { get; set; }

        public CourseGroup? CourseGroups { get; set; }
        public List<Test>? Tests { get; set; }
        public List<Lesson>? Lessons { get; set; }
    }
}

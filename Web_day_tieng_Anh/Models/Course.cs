using System.ComponentModel.DataAnnotations;
namespace Web_day_tieng_Anh.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        [Required, StringLength(100)]
        public string CourseName { get; set; }
        
        public string CourseDescription { get; set; }

        public int Level { get; set; }
        
        [Range(0.01, 10000.00)]
        public decimal Price { get; set; }   

        public int Ratings { get; set; }

        public CourseGroup? CourseGroups { get; set; }
        public List<Test>? Tests { get; set; }
        public List<Lesson>? Lessons { get; set; }
    }
}

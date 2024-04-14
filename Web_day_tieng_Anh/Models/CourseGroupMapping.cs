using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class CourseGroupMapping
    {
        [Key]
        public int MappingId { get; set; }
        public int CourseId { get; set; }
        public int GroupId { get; set; }

        public Course? Courses { get; set; }
        public CourseGroup? CourseGroup { get; set; }
    }
}

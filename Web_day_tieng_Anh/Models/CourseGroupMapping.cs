using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class CourseGroupMapping
    {
        [Key]
        public int mappingId { get; set; }
        public int courseId { get; set; }
        public int groupId { get; set; }

        public Course? courses { get; set; }
        public CourseGroup? courseGroup { get; set; }
    }
}

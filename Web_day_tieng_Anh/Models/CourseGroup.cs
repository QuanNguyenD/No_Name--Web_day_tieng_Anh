using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class CourseGroup
    {
        [Key]
        public int GroupId { get; set; }
        [Required, StringLength(50)]
        public string GroupName { get; set; }
        public string GroupDescription { get; set; }
        public List<Course>? Courses { get; set; }

    }
}

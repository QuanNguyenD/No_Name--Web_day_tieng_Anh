using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class CourseGroup
    {
        [Key]
        public int groupId { get; set; }
        [Required, StringLength(50)]
        public string groupName { get; set; }
        public string groupDescription { get; set; }
        public List<Course>? courses { get; set; }

    }
}

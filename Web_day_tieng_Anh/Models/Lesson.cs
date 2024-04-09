using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class Lesson
    {
        [Key]
        public int lessionId { get; set; }
        public int courseId { get; set; }
        public string lessonName { get; set;}
        public string lessonDescription { get; set; }
        public string VideoUrl { get; set; }
        public string quiz {  get; set; }
        public Course? course { get; set; }

        public List<StudentProgress>? studentProgresses { get; set; }
        public List<Comment>? comments { get; set; }

    }
}

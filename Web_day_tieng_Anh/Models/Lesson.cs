using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class Lesson
    {
        [Key]
        public int LessionId { get; set; }
        public int CourseId { get; set; }
        public string LessonName { get; set;}
        public string LessonDescription { get; set; }
        public string VideoUrl { get; set; }
        public string Quiz {  get; set; }
        public Course? Vourse { get; set; }

        public List<StudentProgress>? StudentProgresses { get; set; }
        public List<Comment>? Comments { get; set; }

    }
}

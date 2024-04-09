namespace Web_day_tieng_Anh.Models
{
    public class Test
    {
        public int testId { get; set; }
        public int courseId { get; set; }
        public string testName { get; set; }
        public string testDescription { get; set; }
        public Course? course { get; set; }

        public List<Question>? question { get; set; }
    }
}

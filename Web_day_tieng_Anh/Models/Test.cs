namespace Web_day_tieng_Anh.Models
{
    public class Test
    {
        public int TestId { get; set; }
        public int CourseId { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public Course? Course { get; set; }

        public List<Question>? Question { get; set; }
    }
}

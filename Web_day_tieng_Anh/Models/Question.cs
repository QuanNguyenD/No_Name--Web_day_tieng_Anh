namespace Web_day_tieng_Anh.Models
{
    public class Question
    {
        public int questionId { get; set; }
        public int testId { get; set; }
        public string questionContent { get; set; }
        public Test? Test { get; set; }

        public List<Answer>? Answers { get; set;}

    }
}

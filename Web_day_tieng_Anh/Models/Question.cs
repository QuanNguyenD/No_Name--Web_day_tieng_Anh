namespace Web_day_tieng_Anh.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        public int TestId { get; set; }
        public string QuestionContent { get; set; }
        public Test? Test { get; set; }

        public List<Answer>? Answers { get; set;}

    }
}

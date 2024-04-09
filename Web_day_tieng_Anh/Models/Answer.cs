namespace Web_day_tieng_Anh.Models
{
    public class Answer
    {
        public int answerId { get; set; }
        public int questionId { get; set; }
        public string answerContent { get; set; }

        public bool isCorrect {  get; set; }
        public Question? Question { get; set; }
    }
}

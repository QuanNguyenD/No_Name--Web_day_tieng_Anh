namespace Web_day_tieng_Anh.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int QuestionId { get; set; }
        public string AnswerContent { get; set; }

        public bool IsCorrect {  get; set; }
        public Question? Question { get; set; }
    }
}

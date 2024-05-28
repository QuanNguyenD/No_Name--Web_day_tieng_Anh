namespace Web_day_tieng_Anh.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public int LessonId { get; set; }
        public string Text { get; set; }
        public string Date { get; set; }
        public string UserId { get; set; } // Add user ID
        public string UserName { get; set; } // Add user name
        public Lesson? Lesson { get; set; }
    }
}

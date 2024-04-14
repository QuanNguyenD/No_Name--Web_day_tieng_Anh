namespace Web_day_tieng_Anh.Models
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentContent { get; set; }
        public int LessionId { get; set; }
        public Lesson? Lesson { get; set; }
    }
}

namespace Web_day_tieng_Anh.Models
{
    public class Comment
    {
        public int commentId { get; set; }
        public string commentContent { get; set; }
        public int lessionId { get; set; }
        public Lesson? Lesson { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class TestScore
    {
        [Key]
        public int ScoreId { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
    }
}

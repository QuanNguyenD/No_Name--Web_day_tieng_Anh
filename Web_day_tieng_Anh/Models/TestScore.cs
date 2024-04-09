using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class TestScore
    {
        [Key]
        public int scoreId { get; set; }
        public int testId { get; set; }
        public Test Test { get; set; }
    }
}

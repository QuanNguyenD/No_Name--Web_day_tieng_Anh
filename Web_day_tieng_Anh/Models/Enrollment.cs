using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Web_day_tieng_Anh.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentsId { get; set; }
        
        public string UserId { get; set; }

        public DateTime EnrollmentsDate { get; set; }
        public decimal TotalPrice { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        
        public List<EnrollmentDetail>? EnrollmentDetail { get; set; }
    }
}


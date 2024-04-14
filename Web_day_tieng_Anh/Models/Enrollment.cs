using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Web_day_tieng_Anh.Models
{
    public class Enrollment
    {
        [Key]
        public int EnrollmentsId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentsDate { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public Course? Course { get; set; }
        public List<StudentProgress>? StudentsProgress { get; set; }
    }
}


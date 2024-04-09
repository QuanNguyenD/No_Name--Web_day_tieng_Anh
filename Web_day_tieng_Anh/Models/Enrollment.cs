using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_day_tieng_Anh.Models;
public class Enrollment
    {
        [Key]    
        public int enrollmentsId { get; set; }
        public int courseId { get; set; }
        public DateTime enrollmentsDate { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        public Course? course { get; set; }
        public List<StudentProgress>? studentsProgress { get; set; }
    }

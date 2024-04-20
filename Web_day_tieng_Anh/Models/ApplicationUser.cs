using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Web_day_tieng_Anh.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public int PhoneNumber { get; set; }

        public string? ImageUrl { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}

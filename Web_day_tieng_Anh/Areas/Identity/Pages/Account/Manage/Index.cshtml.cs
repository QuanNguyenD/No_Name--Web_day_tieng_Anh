// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web_day_tieng_Anh.Models;

namespace Web_day_tieng_Anh.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        
        public string Username { get; set; }

        
        [TempData]
        public string StatusMessage { get; set; }

        
        [BindProperty]
        public InputModel Input { get; set; }

       
        public class InputModel
        {

            
            [Display(Name = "Phone number")]
            [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid Phone Number")]
            public int PhoneNumber { get; set; }

            [Display(Name = "FullName")]
            public string FullName {  get; set; }

            public IFormFile ImageFile { get; set; } // Property for image upload
            public string ImgUrlUser { get; set; }
            [Display(Name = "Date of Birth")]
            [DataType(DataType.Date)]
            public DateTime DateOfBirth { get; set; }

        }
        


        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            
            

            Username = userName;
            Input = new InputModel
            {
                PhoneNumber = user.PhoneNumber,
                //PhoneNumber = phoneNumber,
                
                FullName = user.FullName,
                ImgUrlUser = user.ImageUrl,
                DateOfBirth = (DateTime)user.DateOfBirth


            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
        private async Task<string> SaveImage(IFormFile imageUrl)
        {
            var savePath = Path.Combine("wwwroot/img", imageUrl.FileName);
            using (var fileStream = new FileStream(savePath, FileMode.Create))
            {
                await imageUrl.CopyToAsync(fileStream);
            }
            return "/img/" + imageUrl.FileName;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("ImgUrlUser");
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            if (Input.ImageFile != null)
            {
                // Save the uploaded image and update the user's ImgUrlUser property
                user.ImageUrl = await SaveImage(Input.ImageFile);
            }
            if (Input.PhoneNumber != user.PhoneNumber)
            {
                user.PhoneNumber = Input.PhoneNumber;
            }
            
            if (Input.FullName != user.FullName)
            {
                user.FullName = Input.FullName;
            }
            if (Input.DateOfBirth != user.DateOfBirth)
            {
                user.DateOfBirth = Input.DateOfBirth;
            }

            await _userManager.UpdateAsync(user);
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductReviewAuthentication.Models
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Gender { get; set; }
    }

    public class CreateUserViewModel
    {
        [Required(ErrorMessage = "User name is required")]
        [Display(Name = "User Name:")]
        public string Name { get; set; }

        [Required(ErrorMessage = "User E-mail is required")]
        [Display(Name = "E-mail Address:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Password is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password!!")]
        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [Display(Name = "Gender:")]
        public string Gender { get; set; }
    }

    public class LoginViewModel
    {
        [Required(ErrorMessage = "User E-mail is required")]
        [Display(Name = "E-mail Address:")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Password is required")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }

}

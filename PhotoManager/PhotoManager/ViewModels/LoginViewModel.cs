using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoManager.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter the username.")]
        [StringLength(50, ErrorMessage = "The username must be less than {1} characters.")]
        [Display(Name = "Username:")]
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter the password.")]
        [Display(Name = "Password:")]
        public string Password { get; set; }
    }
}
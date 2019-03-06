using System.ComponentModel.DataAnnotations;

namespace PhotoManager.Model.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Please enter the username.")]
        [StringLength(50, ErrorMessage = "The username must be less than {1} characters.")]
        [Display(Name = "Username:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter the password.")]
        [Display(Name = "Password:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
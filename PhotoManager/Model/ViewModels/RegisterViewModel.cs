using System.ComponentModel.DataAnnotations;

namespace PhotoManager.Model.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Please enter the username.")]
        [Display(Name = "Username:")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please enter the password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Password:")]
        [RegularExpression(@"(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^\w\s]).{6,}", ErrorMessage = "Invalid Password. It must contain minimum six characters, at least one uppercase letter, one lowercase letter, one number and one special character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm the password.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password:")]
        [Compare("Password", ErrorMessage = "Password do not much")]
        public string ConfirmPassword { get; set; }
    }
}
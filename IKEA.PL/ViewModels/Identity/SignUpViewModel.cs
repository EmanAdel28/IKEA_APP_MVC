using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels.Identity
{
    public class SignUpViewModel
    {
        [Required]
        [Display(Name ="First Name")]
        public string Fname { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string Lname { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Confirm Password Does not Match With Password!")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "You must agree to the terms and conditions")]
        public bool IsAgree { get; set; }
      
    }
}

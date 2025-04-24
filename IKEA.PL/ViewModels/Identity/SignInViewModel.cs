using System.ComponentModel.DataAnnotations;

namespace IKEA.PL.ViewModels.Identity
{
    public class SignInViewModel
    {


        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}

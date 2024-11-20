using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.Viewmodels
{
    public class LoginViewmodel
    {

        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "This field is required"), DataType(DataType.Password)]
        public string Password { get; set; } = null!;
    }
}

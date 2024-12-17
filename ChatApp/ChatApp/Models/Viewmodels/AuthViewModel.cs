using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models.ViewModels
{
    public class AuthViewmodel
    {
       
        [Required(ErrorMessage = "This field is required")]
        [EmailAddress(ErrorMessage = "Invalid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This field is required"), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Models.Viewmodels
{
    public class RegisterViewmodel
    {

        public int Id { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [StringLength(50, ErrorMessage = "Max length is 50")]
        public string Name { get; set; } = null!;
        [Required(ErrorMessage = "This field is required")]
        [StringLength(50, ErrorMessage = "Max length is 50")]
        public string Surname { get; set; } = null!;

        [Required(ErrorMessage = "This field is required")]
        [StringLength(50,ErrorMessage ="Max length is 50")]
        [EmailAddress(ErrorMessage ="Invalid email")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "This field is required"), DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "This field is required"), DataType(DataType.Password), NotMapped]
        [Compare("Password")]
        public string ComfirmPassword { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace test_project.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string email1 { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        [MinLength(8)]
        public string password1 { get; set; }

    }
}
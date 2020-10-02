using System.ComponentModel.DataAnnotations;

namespace InitiativesPlus.Application.ViewModels
{
    public class UserForRegisterViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 4, ErrorMessage = "You must specify a password between 4 and 8 characters.")]
        public string Password { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

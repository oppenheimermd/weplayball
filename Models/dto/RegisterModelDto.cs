using System.ComponentModel.DataAnnotations;

namespace WePlayBall.Models.DTO
{
    public class RegisterModelDto
    {

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        [StringLength(15, ErrorMessage = "msg", MinimumLength = 7)]
        public string Username { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "msg", MinimumLength = 5)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(16, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}

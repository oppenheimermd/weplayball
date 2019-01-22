using System.ComponentModel.DataAnnotations;

namespace WePlayBall.Models.DTO
{
    /// <summary>
    /// Used for login via website
    /// </summary>
    public class FrontEndLogin
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

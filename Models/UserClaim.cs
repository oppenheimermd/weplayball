using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WePlayBall.Helpers;

namespace WePlayBall.Models
{
    public class UserClaim
    {
        public UserClaim()
        {
            this.Timestamp = SystemTime.Now();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(25, ErrorMessage = "25 characters maximum")]
        public string ClaimName { get; set; }

        public DateTime Timestamp { get; set; }
    }
}

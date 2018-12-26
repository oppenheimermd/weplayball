using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WePlayBall.Helpers;

namespace WePlayBall.Models
{
    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid();
            this.Timestamp = SystemTime.Now();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid Id { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "msg", MinimumLength = 5)]
        public string FirstName { get; set; }

        [Required]
        //  Unique
        public string Email { get; set; }

        [Required]
        //  Unique
        [StringLength(15, ErrorMessage = "msg", MinimumLength = 7)]
        public string Username { get; set; }

        public DateTime Timestamp { get; }

        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace WePlayBall.Models
{
    public class InstagramItem
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string UserId { get; set; }

        //  format: https://www.instagram.com/p/Bpq3bwzHkag/
        [Required]
        public string Url { get; set; }

        public string Filename { get; set; }

        public bool IsVideo { get; set; } = false;

        public string GetImage()
        {
            return $"/instagram/{Filename}";
        }
    }
}

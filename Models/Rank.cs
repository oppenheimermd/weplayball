using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WePlayBall.Models
{
    public class Rank
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TeamId { get; set; }

        /// <summary>
        /// Rank position
        /// </summary>
        [Required]
        public int Position { get; set; }

        [Required]
        public string GamesPlayed { get; set; }

        [Required]
        public int GamesWon { get; set; }

        [Required]
        public string GamesLost { get; set; }

        [Required]
        public int Points { get; set; }

        [DataMember]
        public virtual SubDivision SubDivision { get; set; }

        [Required]
        [ForeignKey("SubDivision")]
        public int SubDivisionId { get; set; }
    }
}
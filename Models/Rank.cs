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
        public string RankEncoded { get; set; }

        /// <summary>
        /// Rank position
        /// </summary>
        [Required]
        public int Position { get; set; }

        [Required]
        public int GamesPlayed { get; set; }

        [Required]
        public int GamesWon { get; set; }

        [Required]
        public int GamesLost { get; set; }

        [Required]
        public int Points { get; set; }

        [DataMember]
        public virtual SubDivision SubDivision { get; set; }

        [Required]
        [ForeignKey("SubDivision")]
        public int SubDivisionId { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        public string TeamCode { get; set; }

        [Required]
        public string TeamName { get; set; }
    }
}
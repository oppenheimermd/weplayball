
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WePlayBall.Models
{
    public class TeamStat
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "300 characters maximum")]
        public string TeamName { get; set; }

        [Required]
        public int TeamId { get; set; }

        /// <summary>
        /// Team code for home team
        /// </summary>
        [Required]
        [MaxLength(4)]
        public string TeamCode { get; set; }

        public virtual SubDivision SubDivision { get; set; }

        [Required]
        [ForeignKey("SubDivision")]
        public int SubDivisionId { get; set; }

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
        public int BasketsFor { get; set; }

        [Required]
        public int BasketsAganist { get; set; }

        [Required]
        public int PointsDifference { get; set; }

        [Required]
        public int Points { get; set; }

        public string WPyth { get; set; }

        public string WinsOver500 { get; set; }

        public string WinLossPercent { get; set; }

        public string BasketsPerGame { get; set; }

        public string LossPercentage { get; set; }

        public string WinPercentage { get; set; }

    }
}

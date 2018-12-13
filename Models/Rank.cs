using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WePlayBall.Models
{
    public class Rank
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// Rank position
        /// </summary>
        [DataMember]
        [Required]
        public int Position { get; set; }

        [DataMember]
        [Required]
        public string Team { get; set; }

        [Required]
        [DataMember]
        public string GamesPlayed { get; set; }

        [Required]
        [DataMember]
        public int GamesWon { get; set; }

        [Required]
        [DataMember]
        public string GamesLost { get; set; }

        [Required]
        [DataMember]
        public int Points { get; set; }

        [DataMember]
        [Required]
        [MaxLength(4)]
        public string ClubCode { get; set; }

        [Required]
        [DataMember]
        public string Division { get; set; }

        [DataMember]
        [Required]
        [MaxLength(4)]
        public string DivisionCode { get; set; }

        [Required]
        [DataMember]
        public string SubDivision { get; set; }

        [Required]
        [DataMember]
        public string SubDivisionCode { get; set; }

        //  Optimistic Concurrency  Property 
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
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
        [DataMember]
        [Required]
        public int Position { get; set; }

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
        public virtual SubDivision SubDivision { get; set; }

        [DataMember]
        [Required]
        [ForeignKey("SubDivision")]
        public int SubDivisionId { get; set; }

        //  Optimistic Concurrency  Property 
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WePlayBall.Models
{
    public class GameResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public DateTime TimeStamp { get; set; }

        [DataMember]
        [Required]
        public int HomeTeamId { get; set; }

        [DataMember]
        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        [DataMember]
        public string Score { get; set; }

        [DataMember]
        [Required]
        public int WinnerId { get; set; }

        [DataMember]
        public virtual SubDivision SubDivision { get; set; }

        [DataMember]
        [Required]
        [ForeignKey("SubDivision")]
        public int SubDivisionId { get; set; }

        [Required]
        [DataMember]
        public string HashedResult { get; set; }

        //  Optimistic Concurrency  Property 
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}


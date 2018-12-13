using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WePlayBall.Models
{
    public class Fixture
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public DateTime FixtureDate { get; set; }

        [DataMember]
        public Team HomeTeam { get; set; }

        [Required]
        [ForeignKey("HomeTeam")]
        public int HomeTeamId { get; set; }

        [DataMember]
        public Team AwayTeam { get; set; }

        [DataMember]
        [Required]
        [ForeignKey("AwayTeam")]
        public int AwayTeamId { get; set; }

        [DataMember]
        public SubDivision SubDivision { get; set; }

        [DataMember]
        [Required]
        [ForeignKey("SubDivision")]
        public int SubDivisionId { get; set; }


        //  Optimistic Concurrency  Property 
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}

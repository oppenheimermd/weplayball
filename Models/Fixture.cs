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
        public int Id { get; set; }

        [Required]
        public DateTime FixtureDate { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public string HomeTeamName { get; set; }

        [Required]
        public string HomeTeamCode { get; set; }

        [DataMember]
        public int AwayTeamId { get; set; }

        [Required]
        public string AwayTeamName { get; set; }

        [Required]
        public string AwayTeamCode { get; set; }

        public virtual SubDivision SubDivision { get; set; }

        [Required]
        [ForeignKey("SubDivision")]
        public int SubDivisionId { get; set; }

    }
}

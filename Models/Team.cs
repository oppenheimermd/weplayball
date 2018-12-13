using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WePlayBall.Models
{
    public class Team
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        [StringLength(200, ErrorMessage = "300 characters maximum")]
        public string TeamName { get; set; }

        /// <summary>
        /// Team code for home team
        /// </summary>
        [Required]
        [DataMember]
        [MaxLength(4)]
        public string TeamCode { get; set; }

        [DataMember]
        public SubDivision SubDivision { get; set; }

        [DataMember]
        [Required]
        [ForeignKey("SubDivision")]
        public int SubDivisionId { get; set; }

        public ICollection<GameResult> Results { get; set; }

        public ICollection<Fixture> Fixtures { get; set; }

        //  Optimistic Concurrency  Property 
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}

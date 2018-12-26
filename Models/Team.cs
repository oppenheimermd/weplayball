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
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "200 characters maximum")]
        public string TeamName { get; set; }

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

    }
}

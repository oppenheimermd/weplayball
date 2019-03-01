using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WePlayBall.Models
{
    public class Team
    {
        public Team()
        {
            this.HasLogo = false;
        }
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

        [StringLength(256, ErrorMessage = "256 characters maximum")]
        public string Website { get; set; }

        [StringLength(200, ErrorMessage = "200 characters maximum")]
        public string Address { get; set; }

        public string PostCode { get; set; }
        
        [StringLength(240, ErrorMessage = "240 characters maximum")]
        public string About { get; set; }

        public bool HasLogo { get; set; }

        public  string Logo { get; set; }

        public string LogolUrl()
        {
            return $"/TeamLogos/{Logo}";
        }

    }
}

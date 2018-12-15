using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WePlayBall.Models
{
    public class Division
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "100 characters maximum")]
        public string DivisionName { get; set; }

        [Required]
        [MaxLength(4)]
        public string DivisionCode { get; set; }

        public ICollection<SubDivision> SubDivisions { get; set; }
    }
}

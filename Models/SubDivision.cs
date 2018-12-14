using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WePlayBall.Models
{
    public class SubDivision
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int Id { get; set; }

        //  i.e  NL Second Division South 18/19
        [Required]
        public string SubDivisionTitle { get; set; }

        [Required]
        [MaxLength(4)]
        public string SubDivisionCode { get; set; }

        [DataMember]
        public virtual Division Division { get; set; }

        [DataMember]
        [Required]
        [ForeignKey("Division")]
        public int DivisionId { get; set; }
    }
}

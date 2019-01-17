using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WePlayBall.Models
{
    public class ReportTracker
    {
        public ReportTracker()
        {
            this.TimeStamp = DateTime.Now;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }

        [Required]
        [MaxLength(4), MinLength(4)]
        public string ReportTypeCode { get; set; }
    }
}

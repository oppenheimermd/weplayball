using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WePlayBall.Models
{
    public class DataSourceFixture : IDataSource
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "300 characters maximum")]
        [DataMember]
        public string DataSourceDescription { get; set; }

        [Required]
        [StringLength(300, ErrorMessage = "300 characters maximum")]
        [DataMember]
        public string Url { get; set; }

        [Required]
        [DataMember]
        public string Division { get; set; }

        [Required]
        [MaxLength(4)]
        [DataMember]
        public string DivisionCode { get; set; }

        [Required]
        [DataMember]
        public string UrlHash { get; set; }

        [Required]
        [DataMember]
        public string ClassNameNode { get; set; }
        /// <summary>
        /// Last time data was retreived from this data source
        /// </summary>
        [Required]
        [DataMember]
        public DateTime TimeStamp { get; set; }

        //  Optimistic Concurrency  Property 
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}

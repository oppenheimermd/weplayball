using System;
using System.ComponentModel.DataAnnotations;

namespace WePlayBall.Models
{
    public interface IDataSource
    {
        string DataSourceDescription { get; set; }
        string Url { get; set; }
        string Division { get; set; }
        string DivisionCode { get; set; }
        string UrlHash { get; set; }
        string ClassNameNode { get; set; }
        /// <summary>
        /// Last time data was retreived from this data source
        /// </summary>
        DateTime TimeStamp { get; set; }
    }
}

using System;

namespace WePlayBall.Models.DTO
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ResultDataSourceDto
    {
        public int Id { get; set; }
        public string DataSourceDescription { get; set; }
        public string Url { get; set; }
        public string Division { get; set; }
        public string UrlHash { get; set; }
        public string GameDivisionCode { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

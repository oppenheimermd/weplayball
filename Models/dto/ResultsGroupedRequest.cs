using System;
using System.Collections.Generic;

namespace WePlayBall.Models.DTO
{
    public class ResultsGroupedRequest
    {
        public ResultsGroupedRequest()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime TimeStamp { get; }
        public List<GameResultDto> FirstDivision { get; set; }
        public List<GameResultDto> SecondDivision { get; set; }
        public List<GameResultDto> ThirdDivision { get; set; }
    }
}

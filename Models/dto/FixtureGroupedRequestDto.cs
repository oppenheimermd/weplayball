using System;
using System.Collections.Generic;

namespace WePlayBall.Models.DTO
{
    public class FixtureGroupedRequestDto
    {
        public FixtureGroupedRequestDto()
        {
            TimeStamp = DateTime.Now;
        }

        public DateTime TimeStamp { get; }
        public List<FixturesDto> FirstDivision { get; set; }
        public List<FixturesDto> SecondDivision { get; set; }
        public List<FixturesDto> ThirdDivision { get; set; }
    }
}

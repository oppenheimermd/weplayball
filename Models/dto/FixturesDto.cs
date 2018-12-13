using System;

namespace WePlayBall.Models.DTO
{
    public class FixturesDto
    {
        public DateTime FixtureDate { get; set; }
        public string HomeTeam { get; set; }
        public string HomeTeamCode { get; set; }
        public string AwayTeam { get; set; }
        public string AwayTeamCode { get; set; }
        public string Division { get; set; }
        public string DivisionCode { get; set; }
        public string SubDivision { get; set; }
        public string SubDivisionCode { get; set; }
    }
}

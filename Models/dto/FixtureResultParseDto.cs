using System;

namespace WePlayBall.Models.DTO
{
    public class FixtureResultParseDto
    {
        //  parse format: "15-December-2018"
        public DateTime FixtureDate { get; set; }

        //  parse format: "GLL & Aspire London Titans 3"
        public string FixtureHomeTeamName { get; set; }

        //  parse format: "Thames Valley Kings 1"
        public string FixtureAwayTeamName { get; set; }

        public string DivisionName { get; set; }

        public string DivisionCode { get; set; }

        public string SubDivisionName { get; set; }

        public string SubDivisionCode { get; set; }
    }
}

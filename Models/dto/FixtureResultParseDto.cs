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

        //  parse format: "NL Second Division South 18/19 Fixtures***"
        public string SubDivisionParseName { get; set; }

    }
}


using System;

namespace WePlayBall.Models.DTO
{
    /// <summary>
    /// Used for parsing html
    /// </summary>
    public class GameResultParseDto
    {
        //  format: 16-September-2018
        public DateTime TimeStamp { get; set; }

        //  format: Norwich Lowriders
        public string HomeTeamName { get; set; }

        //  format: 	32 - 5
        public string Score { get; set; }

        //  format: The Bears 2
        public string AwayTeamName { get; set; }

        //  format: NL First Division North 17/18 Results
        public string SubDivisionParseName { get; set; }

        //  format: Norwich Lowriders
        public string WinningTeamName { get; set; }
    }
}

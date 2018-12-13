
using System;

namespace WePlayBall.Models.DTO
{
    /// <summary>
    /// Used for parsing html
    /// </summary>
    public class GameResultParseDto
    {
        public DateTime TimeStamp { get; set; }
        public string HomeTeamName { get; set; }
        public string Score { get; set; }
        public string AwayTeamName { get; set; }
        public string Division { get; set; }
        public string SubDivisionName { get; set; }
        public string DivisionCode { get; set; }
        public string WinningTeamName { get; set; }
        public string HashedResult { get; set; }
    }
}

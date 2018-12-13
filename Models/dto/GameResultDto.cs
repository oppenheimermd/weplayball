using System;

namespace WePlayBall.Models.DTO
{
    public class GameResultDto
    {
        public DateTime TimeStamp { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamCode { get; set; }
        public int SubDivisionId { get; set; }
        public string SubDivisionTitle { get; set; }
        public string SubDivisionCode { get; set; }
        public int DivisionId { get; set; }
        public string DivisionTitle { get; set; }
        public string DivisionCode { get; set; }
        public string Score { get; set; }
        public string WinnerTeamName { get; set; }
        public string WinnerTeamCode { get; set; }
    }
}


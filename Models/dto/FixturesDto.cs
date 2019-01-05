using System;

namespace WePlayBall.Models.DTO
{
    public class FixturesDto
    {
        public DateTime FixtureDate { get; set; }
        public string HomeTeamName { get; set; }
        public string HomeTeamCode { get; set; }
        public bool HomeTeamHasLogo { get; set; }
        public string HomeTeamLogo { get; set; }
        public string AwayTeamName { get; set; }
        public string AwayTeamCode { get; set; }
        public bool AwayTeamHasLogo { get; set; }
        public string AwayTeamLogo { get; set; }
        public string Division { get; set; }
        public string DivisionCode { get; set; }
        public string SubDivision { get; set; }
        public string SubDivisionCode { get; set; }

        public static string GetLogolUrl(string logo)
        {
            var _logo = (string.IsNullOrEmpty(logo)) ? string.Empty : $"/TeamLogos/{logo}";
            return _logo;
        }

    }
}

namespace WePlayBall.Models.DTO
{
    public class TeamDto
    {
        //  Team properties
        public string TeamName { get; set; }
        public string TeamCode { get; set; }
        //  Subdivision properties
        public string SubDivisionTitle { get; set; }
        public string SubDivisionCode { get; set; }
        //  Division properties
        public string DivisionName { get; set; }
        public string DivisionCode { get; set; }
        //
        public string Website { get; set; }

        public string Address { get; set; }

        public string PostCode { get; set; }

        public bool HasLogo { get; set; }

        public string Logo { get; set; }

        public string LogolUrl()
        {
            return $"/TeamLogos/{Logo}";
        }

        public TeamStatDto TeamStatDto { get; set; }
    }
}


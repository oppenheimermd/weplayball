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

        //  Stats
        /// <summary>
        /// Rank position
        /// </summary>
        public int Position { get; set; }

        public int GamesPlayed { get; set; }

        public int GamesWon { get; set; }

        public int GamesLost { get; set; }

        public int BasketsFor { get; set; }

        public int BasketsAganist { get; set; }

        public int PointsDifference { get; set; }

        public int Points { get; set; }

        public string WPyth { get; set; }

        public string WinsOver500 { get; set; }

        public string WinLossPercent { get; set; }

        public string BasketsPerGame { get; set; }

        public string LossPercentage { get; set; }

        public string WinPercentage { get; set; }

        public int SubDivisionCount { get; set; }

        public FixturesDto TeamNextMatch { get; set; }
    }
}


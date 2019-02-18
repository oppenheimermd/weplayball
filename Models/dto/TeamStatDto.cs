using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WePlayBall.Models.DTO
{
    public class TeamStatDto
    {
        public int Id { get; set; }

        public string TeamName { get; set; }

        public int TeamId { get; set; }

        /// <summary>
        /// Team code for home team
        /// </summary>
        public string TeamCode { get; set; }

        public int SubDivisionId { get; set; }

        /// <summary>
        /// Rank position
        /// </summary>
        public int Position { get; set; }

        public int GamesPlayed { get; set; }

        public int GamesWon { get; set; }

        public int GamesLost { get; set; }

        public int BasketsFor { get; set; }

        public int BasketsAgainst { get; set; }

        public int PointsDifference { get; set; }

        public int Points { get; set; }

        public string WPyth { get; set; }

        public string WinsOver500 { get; set; }

        public string WinLossPercent { get; set; }

        public string BasketsPerGame { get; set; }

        public string LossPercentage { get; set; }

        public string WinPercentage { get; set; }
    }
}

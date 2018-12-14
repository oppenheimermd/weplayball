
namespace WePlayBall.Models.DTO
{
    public class RankResultParseDto
    {
        public  int Position { get; set; }
        public string TeamName { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int Points { get; set; }
        public string SubDivisionName { get; set; }
        public string DivisionName { get; set; }
        public string DivisionCode { get; set; }
    }
}


namespace WePlayBall.Models.DTO
{
    public class RankResultParseDto
    {
        public  int Position { get; set; }
        public string TeamName { get; set; }
        public int GamesPlayed { get; set; }
        public int GamesWon { get; set; }
        public int GamesLost { get; set; }
        public int BasketsFor { get; set; }
        public int BasketsAganist { get; set; }
        public int PointsDifference { get; set; }
        public int Points { get; set; }
    }
}

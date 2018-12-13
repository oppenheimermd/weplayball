namespace WePlayBall.Models.DTO
{
    public class SubDivisionDto
    {
        public int Id { get; set; }
        public string SubDivisionTitle { get; set; }
        public string SubDivisionCode { get; set; }
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public string DivisionCode { get; set; }
    }
}

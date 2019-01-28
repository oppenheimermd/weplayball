using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WePlayBall.Models.DTO
{
    public class InstaFavDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string InsagramUserId { get; set; }
        //  format: https://www.instagram.com/p/Bpq3bwzHkag/
        public string Url { get; set; }
        public string Filename { get; set; }
        public bool IsVideo { get; set; }
        public string InstagramUrl { get; set; }
    }
}

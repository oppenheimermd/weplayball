using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WePlayBall.Models.DTO
{
    public class StandingsBySubdivisionDto
    {
        public string SubDivisionTitle { get; set; }
        public string SubDivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string DivisionCode { get; set; }
        /// <summary>
        /// Division Number for ordering
        /// </summary>
        public int Division { get; set; }
        public List<TeamStatDto> SubDivisionStats { get; set; }
    }
}

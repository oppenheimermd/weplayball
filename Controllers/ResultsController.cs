using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WePlayBall.Models.DTO;
using WePlayBall.Service;
using WePlayBall.Settings;

namespace WePlayBall.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IWPBService _wpbService;
        private readonly SiteConfig _siteSettings;

        public ResultsController(IWPBService wpbService, SiteConfig siteSettings)
        {
            _wpbService = wpbService;
            _siteSettings = siteSettings;
        }

        //  Get all results
        // GET: api/Results
        //[Authorize(Policy = WpbPolicy.PolicyReadTeamData)]
        [HttpGet]
        public IActionResult Index()
        {

            var fixtures = _wpbService.GetResultsAsDto();

            var filteredRequest = new ResultsGroupedRequest
            {
                FirstDivision = fixtures.Where(x => x.DivisionCode == "DIV1").ToList(),
                SecondDivision = fixtures.Where(x => x.DivisionCode == "DIV2").ToList(),
                ThirdDivision = fixtures.Where(x => x.DivisionCode == "DIV3").ToList()
            };

            return Ok(filteredRequest);
        }
    }
}
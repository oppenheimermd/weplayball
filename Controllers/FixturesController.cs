using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WePlayBall.Models.DTO;
using WePlayBall.Service;
using WePlayBall.Settings;

namespace WePlayBall.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class FixturesController : ControllerBase
    {
        private readonly IWPBService _wpbService;
        private readonly SiteConfig _siteSettings;

        public FixturesController(IWPBService wpbService, SiteConfig siteSettings)
        {
            _wpbService = wpbService;
            _siteSettings = siteSettings;
        }

        // GET: api/Fixtures
        [HttpGet]
        public IActionResult GetAllFixtures()
        {

            var fixtures = _wpbService.GetFixturesAsDto();

            var filteredRequest = new FixtureGroupedRequestDto
            {
                FirstDivision = fixtures.Where(x => x.DivisionCode == "DIV1").ToList(),
                SecondDivision = fixtures.Where(x => x.DivisionCode == "DIV2").ToList(),
                ThirdDivision = fixtures.Where(x => x.DivisionCode == "DIV3").ToList()
            };

            return Ok(filteredRequest);
        }

        [HttpGet]
        [HttpGet("{teamcode}", Name = "Team")]
        public async Task<IActionResult> GetAllMatchesForTeam(string teamCode)
        {

            if (string.IsNullOrEmpty(teamCode))
                return NotFound();

            var team = await _wpbService.GetTeamByTeamCodeDto(teamCode);
            if (team == null)
                return NotFound();

            var fixturesQuery = Task.Run<List<FixturesDto>>(() =>
            {
                var query = _wpbService.GetFixturesAsDtoAll();
                var filter = query.Where(x => (x.AwayTeamCode == team.TeamCode ||
                x.HomeTeamCode == team.TeamCode) && x.FixtureDate >= System.DateTime.Now.Date)
                .OrderBy(x => x.FixtureDate).ToList();
                var entity = (filter.Count >= 1) ? filter : null;
                return entity;
            });

            fixturesQuery.Wait();
            return Ok(fixturesQuery);
        }
    }
}
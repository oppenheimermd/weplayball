using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WePlayBall.Authorization;
using WePlayBall.Helpers;
using WePlayBall.Models;
using WePlayBall.Models.DTO;
using WePlayBall.Service;
using WePlayBall.Settings;

namespace weplayball.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
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
        //[Authorize(Policy = WpbPolicy.PolicyReadTeamData)]
        [HttpGet]
        public IActionResult GetTeams()
        {

            var fixtures = _wpbService.GetFixturesAsDtoAsync();

            var filteredRequest = new FixtureGroupedRequestDto
            {
                FirstDivision = fixtures.Where(x => x.DivisionCode == "DIV1").ToList(),
                SecondDivision = fixtures.Where(x => x.DivisionCode == "DIV2").ToList(),
                ThirdDivision = fixtures.Where(x => x.DivisionCode == "DIV3").ToList()
            };

            return Ok(filteredRequest);
        }
    }
}
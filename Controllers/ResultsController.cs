using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WePlayBall.Models;
using WePlayBall.Models.DTO;
using WePlayBall.Service;
using WePlayBall.Settings;

namespace WePlayBall.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [HttpGet]
        public IActionResult GetAllResults()
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

        //  /api/teams/[teamCode]
        [HttpGet("/results/standings", Name = "TeamStandings")]
        public async Task<IActionResult> GetTeamStandings()
        {

            var allStandings = new List<StandingsBySubdivisionDto>();

            var subDivisions = await _wpbService.GetSubDivisionAllAsync();
            foreach (var subDivision in subDivisions)
            {
                //  Get the ranking info for this subdivision
                var subDivStats = await _wpbService.GetTeamsStatsDtoBySubDivisionAsync(subDivision.Id);

                var standingBySubdivision = new StandingsBySubdivisionDto()
                {
                    SubDivisionTitle = subDivision.SubDivisionTitle,
                    SubDivisionCode = subDivision.SubDivisionCode,
                    DivisionName = subDivision.Division.DivisionName,
                    DivisionCode = subDivision.Division.DivisionCode,
                    Division = GetNumericalDivision(subDivision.Division.DivisionCode),
                    SubDivisionStats = subDivStats
                };
                allStandings.Add(standingBySubdivision);
            };

            allStandings = allStandings.OrderByDescending(x => x.Division).ToList();
            return Ok(allStandings);
        }

        //  /api/teams/[teamCode]
        [HttpGet("{teamCode}", Name = "TeamResults")]
        public async Task<IActionResult> GetTeamResultsAll(string teamCode)
        {
            if (string.IsNullOrEmpty(teamCode))
                return NotFound();

            var team = await _wpbService.GetTeamByTeamCodeDto(teamCode);
            if (team == null)
                return NotFound();

            var query = _wpbService.GetResultsTeamAsDtoAll(teamCode).OrderBy(x => x.TimeStamp);

            return Ok(query);
        }

        public int GetNumericalDivision(string divCode)
        {
            var caseSwitch = divCode;

            switch (caseSwitch)
            {
                case "DIV1":
                    return 1;
                case "DIV2":
                    return 2;
                case "DIV3":
                    return 3;
                default:
                    throw new InvalidOperationException("Division code not found");
            }
        }


    }




}
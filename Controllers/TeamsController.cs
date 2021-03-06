﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WePlayBall.Authorization;
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
    public class TeamsController : ControllerBase
    {
        private readonly IWPBService _wpbService;
        private readonly SiteConfig _siteSettings;

        public TeamsController(IWPBService wpbService, SiteConfig siteSettings)
        {
            _wpbService = wpbService;
            _siteSettings = siteSettings;
        }

        // GET: api/Teams
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams()
        {
            return await _wpbService.GetTeamsAllAsync();
        }

        //  /api/teams/[teamCode]
        [Authorize]
        [HttpGet("{teamcode}", Name = "TeamDetails")]
        public async Task<ActionResult> GetTeamInfo(string teamCode)
        {
            if (string.IsNullOrEmpty(teamCode))
                return NotFound();

            var team = await _wpbService.GetTeamByTeamCodeDto(teamCode);
            var teamStats = await _wpbService.GetTeamStat(teamCode);
            var teamSubdivision = team.SubDivisionCode;
            var subDivCount = await _wpbService.GetSubDivisionCountAsync(teamSubdivision);
            //  get all teams in this subdivision
            var teamPeers = await _wpbService.GetTeamsBySubDivisionAllAsync(team.SubDivisionCode);
            //  Remove the team with the teamCode passed in
            var teamSelf = teamPeers.FirstOrDefault(x => x.TeamCode == teamCode);
            teamPeers.Remove(teamSelf);

            var nextFixture = Task.Run<FixturesDto>(() =>
            {
                var query = _wpbService.GetFixturesAsDtoAll();
                var filter = query.Where(x => (x.AwayTeamCode == team.TeamCode ||
                x.HomeTeamCode == team.TeamCode) && x.FixtureDate >= System.DateTime.Now.Date)
                .OrderBy(x => x.FixtureDate).ToList();
                var entity = (filter.Count >= 1) ? filter[0] : null;
                return entity;
            });

            nextFixture.Wait();

            var lastResult = Task.Run<GameResultDto>(() =>
           {
               var query = _wpbService.GetResultsAsDtoAll();
               var filter = query.Where(x => x.AwayTeamCode == team.TeamCode ||
              x.HomeTeamCode == team.TeamCode)
               .OrderBy(x => x.TimeStamp).ToList();
               var entity = (filter.Count >= 1) ? filter[0] : null;
               return entity;
           });

            lastResult.Wait();

            if (team != null && teamStats != null)
            {
                team = AddTeamStat(teamStats, team);
                team.SubDivisionCount = subDivCount;
                team.TeamNextMatch = nextFixture.Result;
                team.TeamLastResult = lastResult.Result;
                team.Peers = teamPeers;
                return Ok(team);
            }
            else
            {
                return NotFound();
            }
        }

        private TeamDto AddTeamStat(TeamStatDto stat, TeamDto team)
        {
            team.Position = stat.Position;
            team.GamesPlayed = stat.GamesPlayed;
            team.GamesWon = stat.GamesWon;
            team.GamesLost = stat.GamesLost;
            team.BasketsFor = stat.BasketsFor;
            team.BasketsAganist = stat.BasketsAgainst;
            team.PointsDifference = stat.PointsDifference;
            team.Points = stat.Points;
            team.WPyth = stat.WPyth;
            team.WinsOver500 = stat.WinsOver500;
            team.WinLossPercent = stat.WinLossPercent;
            team.BasketsPerGame = stat.BasketsPerGame;
            team.LossPercentage = stat.LossPercentage;
            team.WinPercentage = stat.WinPercentage;
            team.SubDivisionTitle = stat.SubDivisionName;

            return team;
        }

    }
}
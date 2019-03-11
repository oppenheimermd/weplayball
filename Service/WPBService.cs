using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Internal;
using WePlayBall.Data;
using WePlayBall.Models;
using WePlayBall.Models.DTO;
using WePlayBall.Models.Helpers;
using WePlayBall.Settings;

namespace WePlayBall.Service
{
    // ReSharper disable once InconsistentNaming
    public class WPBService : IWPBService
    {
        private readonly WPBDataContext _wpbDataContext;
        private readonly SiteConfig _siteSettings;
        private readonly string _logoFolder;
        private readonly int _logoSize;
        private readonly string _InstagramPhotos;
        private readonly int _photoSize;

        public WPBService(WPBDataContext wpbDataContext, SiteConfig siteSettings, IHostingEnvironment env)
        {
            _wpbDataContext = wpbDataContext;
            _siteSettings = siteSettings;
            _logoFolder = Path.Combine(env.WebRootPath, "teamLogos");
            _InstagramPhotos = Path.Combine(env.WebRootPath, "instagram");
            _logoSize = 100;
            _photoSize = 1100;
        }

        //  Queries

        public PagedResult<Division> GetDivisionsPageable(int? page)
        {
            var pageableDivisions = _wpbDataContext.Divisions
                .OrderByDescending(x => x.DivisionName)
                .AsNoTracking()
                .GetPaged(page ?? 1, int.Parse(_siteSettings.ItemsPerPage));

            return pageableDivisions;
        }

        public async Task<Division> GetDivisionAsync(int? id)
        {
            var division = await _wpbDataContext.Divisions
                .FirstOrDefaultAsync(x => x.Id == id);

            return division;
        }

        public async Task<List<Division>> GetDivisionDropListAsync()
        {
            var divisionList = await _wpbDataContext.Divisions.AsNoTracking().ToListAsync();
            return divisionList;
        }

        public PagedResult<SubDivision> GetSubDivisionsPageable(int? page)
        {
            var pageableSubDivisions = _wpbDataContext.SubDivisions
                .Include("Division")
                .OrderByDescending(x => x.SubDivisionTitle)
                .AsNoTracking()
                .GetPaged(page ?? 1, int.Parse(_siteSettings.ItemsPerPage));

            return pageableSubDivisions;
        }

        public async Task<List<SubDivision>> GetSubDivisionAllAsync()
        {
            var query = await _wpbDataContext.SubDivisions
                .Include("Division")
                .OrderByDescending(x => x.SubDivisionTitle)
                .AsNoTracking()
                .ToListAsync();

            return query;
        }

        public async Task<SubDivision> GetSubDivisionAsync(int? id)
        {
            var subDivision = await _wpbDataContext.SubDivisions
                .Include("Division")
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return subDivision;
        }

        public async Task<int> GetSubDivisionCountAsync(string subDivisionCode)
        {
            var count = 0;
            var subDivCode = subDivisionCode.ToUpper();

            var subdivision = await _wpbDataContext.SubDivisions
                .Include("Division")
                .AsNoTracking()
                .FirstOrDefaultAsync( x => x.SubDivisionCode == subDivCode);

            var query = await _wpbDataContext.Teams
                .Include("SubDivision")
                .Where(x => x.SubDivisionId == subdivision.Id)
                .AsNoTracking().ToListAsync();

            count = (query.Count <= 0 )? 0 : query.Count;
            return count;

        }

        public PagedResult<Team> GetTeamsPageable(int? page)
        {
            var pageableTeam = _wpbDataContext.Teams
                .Include(x => x.SubDivision)
                    .ThenInclude(subdivision => subdivision.Division)
                .OrderBy(x => x.TeamName)
                .AsNoTracking()
                .GetPaged(page ?? 1, int.Parse(_siteSettings.ItemsPerPage));

            return pageableTeam;
        }

        public async Task<List<SubDivision>> GetSubDivisionDropListAsync()
        {
            var subDivisionList = await _wpbDataContext.SubDivisions.AsNoTracking().ToListAsync();
            return subDivisionList;
        }

        public async Task<Team> GetTeamAsync(int? id)
        {
            var team = await _wpbDataContext.Teams
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return team;
        }

        public async Task<Team> GetTeamByTeamName(string teamName)
        {
            var team = await _wpbDataContext.Teams
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TeamName == teamName);

            return team;
        }

        public async Task<Team> GetTeamByTeamCode(string teamCode)
        {
            var team = await _wpbDataContext.Teams
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.TeamCode == teamCode);

            return team;
        }

        public async Task<TeamDto> GetTeamByTeamCodeDto(string teamCode)
        {
            var team = await _wpbDataContext.Teams
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .AsNoTracking()
                .Select(ModelHelpers.AsTeamDto)
                .FirstOrDefaultAsync(x => x.TeamCode == teamCode);

            return team;
        }

        public async Task<List<TeamDto>> GetTeamsAllAsync()
        {
            var teams = await _wpbDataContext.Teams
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .Select(ModelHelpers.AsTeamDto)
                .AsNoTracking().ToListAsync();

            return teams;
        }

        public async Task<List<Team>> GetTeamsAllAdminAsync()
        {
            var teams = await _wpbDataContext.Teams
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .AsNoTracking().ToListAsync();

            return teams;
        }

        public async Task<DataSourceFixture> GetFixtureDataSource(int? id)
        {
            var dataSource = await _wpbDataContext.DataSourceFixtures
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id.Value);
            return dataSource;
        }

        public async Task<List<DataSourceFixture>> GetFixtureDataSources()
        {
            var results = await _wpbDataContext.DataSourceFixtures
                .AsNoTracking().ToListAsync();
            return results;
        }

        public async Task<SubDivision> GetSubDivisionByName(string subdivisionName)
        {
            var subdivision = await _wpbDataContext.SubDivisions
                .Where(x => x.SubDivisionTitle == subdivisionName)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return subdivision;
        }

        public async Task<IEnumerable<Fixture>> GetFixturesAsync()
        {
            var fixtures = await _wpbDataContext.Fixtures
                .AsNoTracking().ToListAsync();
            return fixtures;
        }

        /*public async Task<IEnumerable<FixturesDto>> GetFixturesAsDtoAsync()
        {
            var timeStampNow = DateTime.Now;

            var fixtures = await _wpbDataContext.Fixtures
                .Where( x => x.FixtureDate >= timeStampNow)
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .Select(ModelHelpers.AsFixtureDto)
                .AsNoTracking().ToListAsync();

            return fixtures;
        }*/

        public List<FixturesDto> GetFixturesAsDto()
        {
            var timestampNow = DateTime.Now;
            var timestampSeven = timestampNow.AddDays(7);

            var query = (from f in _wpbDataContext.Fixtures
                where f.FixtureDate >= timestampNow && f.FixtureDate <= timestampSeven
                join homeTeam in _wpbDataContext.Teams on f.HomeTeamId equals homeTeam.Id
                join awayTeam in _wpbDataContext.Teams on f.AwayTeamId equals awayTeam.Id
                join subDiv in _wpbDataContext.SubDivisions on f.SubDivisionId equals subDiv.Id
                join div in _wpbDataContext.Divisions on subDiv.DivisionId equals div.Id
                select new FixturesDto
                {
                    FixtureDate = f.FixtureDate,
                    HomeTeamName = homeTeam.TeamName,
                    HomeTeamCode = homeTeam.TeamCode,
                    HomeTeamHasLogo = homeTeam.HasLogo,
                    HomeTeamLogo = FixturesDto.GetLogolUrl(homeTeam.Logo),
                    AwayTeamName = awayTeam.TeamName,
                    AwayTeamCode = awayTeam.TeamCode,
                    AwayTeamHasLogo = awayTeam.HasLogo,
                    AwayTeamLogo = FixturesDto.GetLogolUrl(awayTeam.Logo),
                    Division = div.DivisionName,
                    DivisionCode = div.DivisionCode,
                    SubDivision = subDiv.SubDivisionTitle,
                    SubDivisionCode = subDiv.SubDivisionCode
                });

            return query.ToList();
        }

        public List<FixturesDto> GetFixturesAsDtoAll()
        {

            var query = (from f in _wpbDataContext.Fixtures
                         //where f.FixtureDate >= timestampNow && f.FixtureDate <= timestampSeven
                         join homeTeam in _wpbDataContext.Teams on f.HomeTeamId equals homeTeam.Id
                         join awayTeam in _wpbDataContext.Teams on f.AwayTeamId equals awayTeam.Id
                         join subDiv in _wpbDataContext.SubDivisions on f.SubDivisionId equals subDiv.Id
                         join div in _wpbDataContext.Divisions on subDiv.DivisionId equals div.Id
                         select new FixturesDto
                         {
                             FixtureDate = f.FixtureDate,
                             HomeTeamName = homeTeam.TeamName,
                             HomeTeamCode = homeTeam.TeamCode,
                             HomeTeamHasLogo = homeTeam.HasLogo,
                             HomeTeamLogo = FixturesDto.GetLogolUrl(homeTeam.Logo),
                             AwayTeamName = awayTeam.TeamName,
                             AwayTeamCode = awayTeam.TeamCode,
                             AwayTeamHasLogo = awayTeam.HasLogo,
                             AwayTeamLogo = FixturesDto.GetLogolUrl(awayTeam.Logo),
                             Division = div.DivisionName,
                             DivisionCode = div.DivisionCode,
                             SubDivision = subDiv.SubDivisionTitle,
                             SubDivisionCode = subDiv.SubDivisionCode
                         });

            return query.ToList();
        }

        public async Task<IEnumerable<FixturesDto>> AddTeamLogosAsync(IEnumerable<FixturesDto> fixturesDtosAsList)
        {
            var addTeamLogo = fixturesDtosAsList as FixturesDto[] ?? fixturesDtosAsList.ToArray();
            foreach (var team in addTeamLogo)
            {
                var homeTeam = await GetTeamByTeamCode(team.HomeTeamCode);
                var awayTeam = await GetTeamByTeamCode(team.AwayTeamCode);

                team.HomeTeamLogo = homeTeam.Logo;
                team.HomeTeamHasLogo = homeTeam.HasLogo;
                team.AwayTeamLogo = awayTeam.Logo;
                team.AwayTeamHasLogo = awayTeam.HasLogo;

            }

            return addTeamLogo;
        }



        public PagedResult<Fixture> GetFixturePageable(int? page)
        {
            var timeStamp = DateTime.Now;
            var pageableFixtures = _wpbDataContext.Fixtures
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .Where(x => x.FixtureDate >= timeStamp)
                .OrderBy(x => x.FixtureDate)
                .AsNoTracking()
                .GetPaged(page ?? 1, int.Parse(_siteSettings.ItemsPerPage));

            return pageableFixtures;
        }

        public async Task<DataSourceResult> GetResultDataSource(int? id)
        {
            var dataSource = await _wpbDataContext.DataSourceResults
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id.Value);
            return dataSource;
        }

        public async Task<IEnumerable<DataSourceResult>> GetAllResultDataSourceAsync()
        {
            var datasources = await _wpbDataContext.DataSourceResults
                .AsNoTracking().ToListAsync();
            return datasources;
        }

        public Task<bool> GameResultExistAsync(string encodedResult)
        {
            return Task.Run<bool>(() =>
            {
                return _wpbDataContext.GameResults.Any(x => x.EncodedResult == encodedResult);
            });
        }

        public async Task<IEnumerable<GameResult>> GetGameResultsAsync()
        {
            var games = await _wpbDataContext.GameResults
                .AsNoTracking().ToListAsync();
            return games;
        }

        public PagedResult<GameResult> GetGameResultsPageable(int? page)
        {
            var pageableFixtures = _wpbDataContext.GameResults
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .OrderByDescending(x => x.TimeStamp)
                .AsNoTracking()
                .GetPaged(page ?? 1, int.Parse(_siteSettings.ItemsPerPage));

            return pageableFixtures;
        }

        public async Task<DataSourceRanking> GetRankDataSource(int? id)
        {
            var dataSource = await _wpbDataContext.DataSourceRankings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id.Value);
            return dataSource;
        }

        public PagedResult<Team> GetTeamsBySubDivisionPageable(int? page, int Id)
        {
            var teams = _wpbDataContext.Teams
                .Include(x => x.SubDivision)
                .ThenInclude(x => x.Division)
                .OrderByDescending(x => x.TeamName)
                .Where(x => x.SubDivision.Id == Id)
                .AsNoTracking()
                .GetPaged(page ?? 1, int.Parse(_siteSettings.ItemsPerPage));

            return teams;
        }

        public async Task<List<TeamExtraLightDto>> GetTeamsBySubDivisionAllAsync(string subdivisionCode)
        {
            var subdivToUpper = subdivisionCode.ToUpper();

            var query = await _wpbDataContext.Teams
                .Include(x => x.SubDivision)
                .ThenInclude(x => x.Division)
                .OrderByDescending(x => x.TeamName)
                .Where(x => x.SubDivision.SubDivisionCode == subdivToUpper)
                .AsNoTracking()
                .Select(ModelHelpers.AsTeamExtraLightDto).ToListAsync();

            return query;
        }

        public async Task<List<DataSourceRanking>> GetRankingDataSources()
        {
            var results = await _wpbDataContext.DataSourceRankings
                .AsNoTracking().ToListAsync();
            return results;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _wpbDataContext.Users.FirstOrDefaultAsync(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            return !VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt) ? null : user;

            // authentication successful
        }

        public async Task<IEnumerable<UserClaim>> GetUserClaimsAsync(Guid userId)
        {
            var claims = await _wpbDataContext.UserClaims
                .Where(x => x.UserId == userId)
                .AsNoTracking().ToListAsync();
            return claims;
        }

        public List<GameResultDto> GetResultsAsDto()
        {
            var timestampNow = DateTime.Now;
            //  We only want the "last" 14days
            var timestampSeven = timestampNow.AddDays(-14);

            var query = (from g in _wpbDataContext.GameResults
                where g.TimeStamp >= timestampSeven
                         join homeTeam in _wpbDataContext.Teams on g.HomeTeamId equals homeTeam.Id
                join awayTeam in _wpbDataContext.Teams on g.AwayTeamId equals awayTeam.Id
                join subDiv in _wpbDataContext.SubDivisions on g.SubDivisionId equals subDiv.Id
                join div in _wpbDataContext.Divisions on subDiv.DivisionId equals div.Id
                select new GameResultDto
                {
                    TimeStamp = g.TimeStamp,
                    HomeTeamName = homeTeam.TeamName,
                    HomeTeamCode = homeTeam.TeamCode,
                    HomeTeamHasLogo = homeTeam.HasLogo,
                    HomeTeamLogo = GameResultDto.GetLogolUrl(homeTeam.Logo),
                    AwayTeamName = awayTeam.TeamName,
                    AwayTeamCode = awayTeam.TeamCode,
                    AwayTeamHasLogo = awayTeam.HasLogo,
                    AwayTeamLogo = GameResultDto.GetLogolUrl(awayTeam.Logo),
                    Division = div.DivisionName,
                    DivisionCode = div.DivisionCode,
                    SubDivision = subDiv.SubDivisionTitle,
                    SubDivisionCode = subDiv.SubDivisionCode,
                    Score = g.Score,
                    WinnerTeamName = g.WinningTeamName,
                    WinnerTeamCode = g.WinningTeamCode
                });

            return query.ToList();
        }

        public List<GameResultDto> GetResultsAsDtoAll()
        {

            var query = (from g in _wpbDataContext.GameResults
                         join homeTeam in _wpbDataContext.Teams on g.HomeTeamId equals homeTeam.Id
                         join awayTeam in _wpbDataContext.Teams on g.AwayTeamId equals awayTeam.Id
                         join subDiv in _wpbDataContext.SubDivisions on g.SubDivisionId equals subDiv.Id
                         join div in _wpbDataContext.Divisions on subDiv.DivisionId equals div.Id
                         select new GameResultDto
                         {
                             TimeStamp = g.TimeStamp,
                             HomeTeamName = homeTeam.TeamName,
                             HomeTeamCode = homeTeam.TeamCode,
                             HomeTeamHasLogo = homeTeam.HasLogo,
                             HomeTeamLogo = GameResultDto.GetLogolUrl(homeTeam.Logo),
                             AwayTeamName = awayTeam.TeamName,
                             AwayTeamCode = awayTeam.TeamCode,
                             AwayTeamHasLogo = awayTeam.HasLogo,
                             AwayTeamLogo = GameResultDto.GetLogolUrl(awayTeam.Logo),
                             Division = div.DivisionName,
                             DivisionCode = div.DivisionCode,
                             SubDivision = subDiv.SubDivisionTitle,
                             SubDivisionCode = subDiv.SubDivisionCode,
                             Score = g.Score,
                             WinnerTeamName = g.WinningTeamName,
                             WinnerTeamCode = g.WinningTeamCode
                         });

            return query.ToList();
        }


        public List<GameResultDto> GetResultsTeamAsDtoAll(string teamCode)
        {

            var query = (from g in _wpbDataContext.GameResults
                         join homeTeam in _wpbDataContext.Teams on g.HomeTeamId equals homeTeam.Id
                         join awayTeam in _wpbDataContext.Teams on g.AwayTeamId equals awayTeam.Id
                         join subDiv in _wpbDataContext.SubDivisions on g.SubDivisionId equals subDiv.Id
                         join div in _wpbDataContext.Divisions on subDiv.DivisionId equals div.Id
                         where g.AwayTeamCode == teamCode || g.HomeTeamCode == teamCode
                         select new GameResultDto
                         {
                             TimeStamp = g.TimeStamp,
                             HomeTeamName = homeTeam.TeamName,
                             HomeTeamCode = homeTeam.TeamCode,
                             HomeTeamHasLogo = homeTeam.HasLogo,
                             HomeTeamLogo = GameResultDto.GetLogolUrl(homeTeam.Logo),
                             AwayTeamName = awayTeam.TeamName,
                             AwayTeamCode = awayTeam.TeamCode,
                             AwayTeamHasLogo = awayTeam.HasLogo,
                             AwayTeamLogo = GameResultDto.GetLogolUrl(awayTeam.Logo),
                             Division = div.DivisionName,
                             DivisionCode = div.DivisionCode,
                             SubDivision = subDiv.SubDivisionTitle,
                             SubDivisionCode = subDiv.SubDivisionCode,
                             Score = g.Score,
                             WinnerTeamName = g.WinningTeamName,
                             WinnerTeamCode = g.WinningTeamCode
                         });

            return query.ToList();
        }

        /*
        public PagedResult<Team> GetTeamsBySubDivisionPageable(int? page, int Id)
        {
            var teams = _wpbDataContext.Teams
                .Include(x => x.SubDivision)
                .ThenInclude(x => x.Division)
                .OrderByDescending(x => x.TeamName)
                .Where(x => x.SubDivision.Id == Id)
                .AsNoTracking()
                .GetPaged(page ?? 1, int.Parse(_siteSettings.ItemsPerPage));

            return teams;
        }
         */


        public async Task<List<TeamStat>> GetTeamsStatsBySubDivisionAsync(int subDivId)
        {

            var results = await _wpbDataContext.TeamStats
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .Where(x => x.SubDivision.Id == subDivId)
                .OrderBy(x => x.Position)
                .AsNoTracking().ToListAsync();

            return results;
        }

        public async Task<List<TeamStatDto>> GetTeamsStatsDtoBySubDivisionAsync(int subDivId)
        {

            var results = await _wpbDataContext.TeamStats
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .Where(x => x.SubDivision.Id == subDivId)
                .OrderBy(x => x.Position)
                .Select(ModelHelpers.AsTeamStatDto)
                .AsNoTracking().ToListAsync();

            return results;
        }




        public async Task<IEnumerable<TeamStat>> GetStatsAllAsync()
        {
            var query = await _wpbDataContext.TeamStats
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .AsNoTracking().ToListAsync();
            return query;
        }

        public async Task<TeamStatDto> GetTeamStat(string teamCode)
        {
            var query = await _wpbDataContext.TeamStats
                .Include(x => x.SubDivision)
                .ThenInclude(subdivision => subdivision.Division)
                .Select(ModelHelpers.AsTeamStatDto)
                .FirstOrDefaultAsync(x => x.TeamCode == teamCode);

            return query;
        }

        public async Task<ReportTracker> GetLastStatReportRun()
        {
            var query = await _wpbDataContext.ReportTracking
                .Where( x => x.ReportTypeCode == ModelHelpers.REPORT_STAT)
                .OrderByDescending(x => x.TimeStamp)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return query;
        }

        public async Task<ReportTracker> GetLastResultsReportRun()
        {
            var query = await _wpbDataContext.ReportTracking
                .Where(x => x.ReportTypeCode == ModelHelpers.REPORT_RSLT)
                .OrderByDescending(x => x.TimeStamp)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return query;
        }

        public async Task<bool> UsernameUnique(string username)
        {
            var usernameToLower = username.ToLower();

            var query = await _wpbDataContext.Users
                .Where(x => x.Username == usernameToLower)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            var isUnique = (query == null);
            return isUnique;
        }

        public async Task<bool> EmailUnique(string email)
        {
            var emailToLower = email.ToLower();

            var query = await _wpbDataContext.Users
                .Where(x => x.Email == emailToLower)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            var isUnique = (query == null);
            return isUnique;
        }

        public PagedResult<InstagramItem> GetInstgramFavsPageable(int? page)
        {
            var pageableDivisions = _wpbDataContext.InstagramItems
                .OrderByDescending(x => x.Date)
                .AsNoTracking()
                .GetPaged(page ?? 1, int.Parse(_siteSettings.ItemsPerPage));

            return pageableDivisions;
        }

        public async Task<bool> InstagramPhotoUnique(string imageSourceUrl)
        {
            var query = await _wpbDataContext.InstagramItems
                .Where(x => x.Url == imageSourceUrl)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            var isUnique = (query == null);
            return isUnique;
        }

        public async Task<List<InstaFavDto>> GetInstaFavAllAsync()
        {
            var query = await _wpbDataContext.InstagramItems
                .Where( x => x.IsVideo == false)
                .OrderByDescending(x => x.Date)
                .Select(ModelHelpers.AsInstaFavDto)
                .AsNoTracking().ToListAsync();

            return query;
        }

        public async Task<InstagramItem> GetInstagramItemAsync(int id)
        {
            var query = await _wpbDataContext.InstagramItems
                .Where( x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return query;
        }

        //  Persistence

        public async Task CreateDivisionAsync(Division division)
        {
            _wpbDataContext.Divisions.Add(division);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateDivisionAsync(Division division)
        {
            try
            {
                _wpbDataContext.Update(division);
                await _wpbDataContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException err)
            {
                var error = err.ToString();

                if (!DivisionExists(division.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                };
            }
        }

        public async Task CreateSubDivisionAsync(SubDivision subdivision)
        {
            _wpbDataContext.SubDivisions.Add(subdivision);
            await _wpbDataContext.SaveChangesAsync();
        }


        public async Task<bool> UpdateSubDivisionAsync(SubDivision subDivision)
        {
            try
            {
                _wpbDataContext.Update(subDivision);
                await _wpbDataContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException err)
            {
                var error = err.ToString();

                if (!SubDivisionExists(subDivision.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                };
            }
        }

        public async Task CreateTeamAsync(Team team)
        {
            _wpbDataContext.Teams.Add(team);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task<bool> UpdateTeamAsync(Team team)
        {
            try
            {
                _wpbDataContext.Update(team);
                await _wpbDataContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException err)
            {
                var error = err.ToString();

                if (!TeamExists(team.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                };
            }
        }

        public async Task CreateFixtureDataSourceAsync(DataSourceFixture dataSourceFixture)
        {
            _wpbDataContext.DataSourceFixtures.Add(dataSourceFixture);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task CreateFixtureAsync(Fixture fixture)
        {
            _wpbDataContext.Fixtures.Add(fixture);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task DeleteFixtureAsync(Fixture fixture)
        {
            var fixtureToDelete = await _wpbDataContext.Fixtures.FindAsync(fixture.Id);
            _wpbDataContext.Fixtures.Remove(fixtureToDelete);
            await _wpbDataContext.SaveChangesAsync();
        }


        public async Task CreateResultDataSourceAsync(DataSourceResult dataSourceResult)
        {
            _wpbDataContext.DataSourceResults.Add(dataSourceResult);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task CreateGameResultAsync(GameResult gameResult)
        {
            _wpbDataContext.GameResults.Add(gameResult);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task DeleteGameResultAsync(GameResult gameResult)
        {
            var resultToDelete = await _wpbDataContext.GameResults.FindAsync(gameResult.Id);
            _wpbDataContext.GameResults.Remove(resultToDelete);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task CreateRankingDataSourceAsync(DataSourceRanking dataSourceRanking)
        {
            _wpbDataContext.DataSourceRankings.Add(dataSourceRanking);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task CreateTeamStatAsync(TeamStat stat)
        {
            _wpbDataContext.TeamStats.Add(stat);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task CreateReportHistory(ReportTracker reportTracker)
        {
            _wpbDataContext.ReportTracking.Add(reportTracker);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task CreateInstagramItemAsync(InstagramItem item)
        {
            _wpbDataContext.InstagramItems.Add(item);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task<User> CreateUserAsync(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _wpbDataContext.Users.Add(user);
            await _wpbDataContext.SaveChangesAsync();

            return user;
        }

        public async Task AddUserClaimAsync(UserClaim userClaim)
        {
            _wpbDataContext.UserClaims.Add(userClaim);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task DeleteTeamStatAsync(TeamStat teamStat)
        {
            var query = await _wpbDataContext.TeamStats.FindAsync(teamStat.Id);
            _wpbDataContext.TeamStats.Remove(query);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task DeleteInstagramItemtAsync(InstagramItem item)
        {
            var resultToDelete = await _wpbDataContext.InstagramItems.FindAsync(item.Id);
            _wpbDataContext.InstagramItems.Remove(resultToDelete);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task<string> SaveTeamLogoAsync(IFormFile teamLogo, string newFilename)
        {
            var imageFilename = "";

            using (var ms = new MemoryStream())
            {
                teamLogo.CopyTo(ms);
                var fileBytes = ms.ToArray();
                imageFilename = await SaveTeamLogoFileAsync(fileBytes, teamLogo.FileName, newFilename).ConfigureAwait(true);
            }

            return imageFilename;
        }

        private async Task<string> SaveTeamLogoFileAsync(byte[] bytes, string fileName, string newFileName)
        {
            return await Task.Run<string>(() =>
            {
                //var newFileName = DateTime.UtcNow.Ticks.ToString();

                var ext = Path.GetExtension(fileName);

                var relative = $"{newFileName}{ext}";
                var absolute = Path.Combine(_logoFolder, relative);
                var dir = Path.GetDirectoryName(absolute);

                Directory.CreateDirectory(dir);
                using (var image = new MagickImage(bytes))
                {
                    //  set just the width to maintain aspect ratio
                    image.Resize(_logoSize, 0);
                    image.Write(absolute);
                }

                //  add additional pictures
                return  newFileName + ext;
            });
        }

        public async Task<string> SaveInstagramPhotooAsync(IFormFile image, bool isVideo)
        {
            var imageFilename = "";

            using (var ms = new MemoryStream())
            {
                image.CopyTo(ms);
                var fileBytes = ms.ToArray();
                imageFilename = await SaveInstagramFileAsync(fileBytes, image.FileName, isVideo).ConfigureAwait(true);
            }

            return imageFilename;
        }

        private async Task<string> SaveInstagramFileAsync(byte[] bytes, string filename, bool isVideo)
        {
            return await Task.Run<string>(() =>
            {
                var newFileName = DateTime.UtcNow.Ticks.ToString();

                var ext = Path.GetExtension(filename);

                var relative = $"{newFileName}{ext}";
                var absolute = Path.Combine(_InstagramPhotos, relative);
                var dir = Path.GetDirectoryName(absolute);

                Directory.CreateDirectory(dir);

                if (isVideo)
                {
                    // Save the uploaded file to "UploadedFiles" folder
                    File.WriteAllBytesAsync(absolute, bytes).Wait();
                }
                else
                {
                    using (var image = new MagickImage(bytes))
                    {
                        //  set just the width to maintain aspect ratio
                        image.Resize(_photoSize, 0);
                        image.Write(absolute);

                    }
                }



                //  add additional pictures
                return newFileName + ext;
            });
        }

        //  Helpers

        private bool DivisionExists(int id)
        {
            return _wpbDataContext.Divisions.Any(e => e.Id == id);
        }

        private bool SubDivisionExists(int id)
        {
            return _wpbDataContext.SubDivisions.Any(e => e.Id == id);
        }

        public bool SubdivisionCodeExist(string subDivCode)
        {
            var subDivCodeToLower = subDivCode.ToLower();
            return _wpbDataContext.SubDivisions.Any(x => x.SubDivisionCode == subDivCodeToLower);
        }

        public bool TeamCodeExist(string teamCode)
        {
            var teamCodeToLower = teamCode.ToLower();
            return _wpbDataContext.Teams.Any(x => x.TeamCode == teamCodeToLower);
        }

        private bool TeamExists(int id)
        {
            return _wpbDataContext.Teams.Any(e => e.Id == id);
        }


        //  See:    https://github.com/cornflourblue/aspnet-core-registration-login-api/blob/master/Services/UserService.cs
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}

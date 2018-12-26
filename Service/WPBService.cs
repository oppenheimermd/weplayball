using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WePlayBall.Data;
using WePlayBall.Helpers;
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

        public WPBService(WPBDataContext wpbDataContext, SiteConfig siteSettings)
        {
            _wpbDataContext = wpbDataContext;
            _siteSettings = siteSettings;
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

        public async Task<SubDivision> GetSubDivisionAsync(int? id)
        {
            var subDivision = await _wpbDataContext.SubDivisions
                .Include("Division")
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return subDivision;
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

        public async Task<List<Team>> GetTeamsAllAsync()
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

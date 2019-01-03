using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WePlayBall.Data;
using WePlayBall.Models;
using WePlayBall.Models.DTO;
using WePlayBall.Models.Helpers;

namespace WePlayBall.Service
{
    // ReSharper disable once InconsistentNaming
    public interface IWPBService
    {
        //  Queries

        /// <summary>
        /// Get <see cref="Division"/>(s) as pageable
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PagedResult<Division> GetDivisionsPageable(int? page);

        /// <summary>
        /// Get <see cref="Division"/> entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Division> GetDivisionAsync(int? id);

        /// <summary>
        /// <see cref="Division"/> DropDown list data
        /// </summary>
        /// <returns></returns>
        Task<List<Division>> GetDivisionDropListAsync();

        /// <summary>
        /// Get <see cref="SubDivision"/>(s) as pageable.  Include <see cref="Division"/>
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PagedResult<SubDivision> GetSubDivisionsPageable(int? page);

        /// <summary>
        /// Get and instance of a <see cref="SubDivision"/> entity.  Includes <see cref="Division"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<SubDivision> GetSubDivisionAsync(int? id);

        /// <summary>
        /// Get <see cref="Team"/>(s) as pageable.  Includes <see cref="Division"/> and <see cref="SubDivision"/>
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PagedResult<Team> GetTeamsPageable(int? page);

        /// <summary>
        /// <see cref="SubDivision"/> DropDown list data
        /// </summary>
        /// <returns></returns>
        Task<List<SubDivision>> GetSubDivisionDropListAsync();

        /// <summary>
        /// Get a <see cref="Team"/> entity.  Includes <see cref="Division"/> and <see cref="SubDivision"/>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Team> GetTeamAsync(int? id);

        /// <summary>
        /// Get a <see cref="Team"/> by team name query.  Includes <see cref="Division"/> and <see cref="SubDivision"/>
        /// </summary>
        /// <param name="teamName"></param>
        /// <returns></returns>
        Task<Team> GetTeamByTeamName(string teamName);

        /// <summary>
        /// Get all <see cref="TeamDto"/>(s) by team name query.  Includes <see cref="Division"/> and <see cref="SubDivision"/>
        /// </summary>
        /// <returns></returns>
        Task<List<TeamDto>> GetTeamsAllAsync();

        /// <summary>
        /// Get a <see cref="DataSourceFixture"/> entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataSourceFixture> GetFixtureDataSource(int? id);

        /// <summary>
        /// Returns a list of <see cref="DataSourceFixture"/>
        /// </summary>
        /// <returns></returns>
        Task<List<DataSourceFixture>> GetFixtureDataSources();

        /// <summary>
        /// Get a <see cref="SubDivision"/> by name query
        /// </summary>
        /// <param name="subdivisionName"></param>
        /// <returns></returns>
        Task<SubDivision> GetSubDivisionByName(string subdivisionName);

        /// <summary>
        /// Get all <see cref="Fixture"/>(s).
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Fixture>> GetFixturesAsync();

        /// <summary>
        /// Get <see cref="Fixture"/>(s) as pageable.  Includes <see cref="Division"/> and <see cref="SubDivision"/>
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PagedResult<Fixture> GetFixturePageable(int? page);


        /// <summary>
        /// Get a <see cref="DataSourceResult"/> entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataSourceResult> GetResultDataSource(int? id);

        /// <summary>
        /// Get enumerable collection of <see cref="DataSourceResult"/>
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DataSourceResult>> GetAllResultDataSourceAsync();

        /// <summary>
        /// Determines if a <see cref="GameResult"/> already exist in the database
        /// </summary>
        /// <param name="encodedResult"></param>
        /// <returns></returns>
        Task<bool> GameResultExistAsync(string encodedResult);

        /// <summary>
        /// Get all <see cref="GameResult"/>(s)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<GameResult>> GetGameResultsAsync();

        /// <summary>
        /// Get <see cref="GameResult"/>(s) as pageable.  Includes <see cref="Division"/> and <see cref="SubDivision"/>
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        PagedResult<GameResult> GetGameResultsPageable(int? page);

        /// <summary>
        /// Get a <see cref="DataSourceRanking"/> entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<DataSourceRanking> GetRankDataSource(int? id);

        /// <summary>
        /// Get teams by <see cref="SubDivision"/> Id. Includes <see cref="SubDivision"/>
        /// </summary>
        /// <param name="page"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        PagedResult<Team> GetTeamsBySubDivisionPageable(int? page, int Id);

        /// <summary>
        /// Get all <see cref="DataSourceRanking"/> entities
        /// </summary>
        /// <returns></returns>
        Task<List<DataSourceRanking>> GetRankingDataSources();

        /// <summary>
        /// Authenticate a <see cref="User"/>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> AuthenticateAsync(string username, string password);

        /// <summary>
        /// Get all <see cref="UserClaim"/> for user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<UserClaim>> GetUserClaimsAsync(Guid userId);

        //  Persistence

        /// <summary>
        /// Create a new instance of a <see cref="Division"/> entity
        /// </summary>
        /// <param name="division"></param>
        /// <returns></returns>
        Task CreateDivisionAsync(Division division);

        /// <summary>
        /// Update a <see cref="Division"/> entity
        /// </summary>
        /// <param name="division"></param>
        /// <returns></returns>
        Task<bool> UpdateDivisionAsync(Division division);

        /// <summary>
        /// Create a new instance of a <see cref="SubDivision"/> entity
        /// </summary>
        /// <param name="subdivision"></param>
        /// <returns></returns>
        Task CreateSubDivisionAsync(SubDivision subdivision);

        /// <summary>
        /// Update a <see cref="SubDivision"/> Entity
        /// </summary>
        /// <param name="subDivision"></param>
        /// <returns></returns>
        Task<bool> UpdateSubDivisionAsync(SubDivision subDivision);

        /// <summary>
        /// Create a new instance of a <see cref="Team"/>
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        Task CreateTeamAsync(Team team);

        /// <summary>
        /// Update a <see cref="Team"/> Entity
        /// </summary>
        /// <param name="team"></param>
        /// <returns></returns>
        Task<bool> UpdateTeamAsync(Team team);

        /// <summary>
        /// Create a new instance of a <see cref="DataSourceFixture"/>
        /// </summary>
        /// <param name="dataSourceFixture"></param>
        /// <returns></returns>
        Task CreateFixtureDataSourceAsync(DataSourceFixture dataSourceFixture);

        /// <summary>
        /// Create a new instance of a <see cref="Fixture"/> entity.
        /// </summary>
        /// <param name="fixture"></param>
        /// <returns></returns>
        Task CreateFixtureAsync(Fixture fixture);

        /// <summary>
        /// Remove and instance of <see cref="Fixture"/>
        /// </summary>
        /// <param name="fixture"></param>
        /// <returns></returns>
        Task DeleteFixtureAsync(Fixture fixture);

        /// <summary>
        /// Create an instance of a <see cref="DataSourceResult"/>
        /// </summary>
        /// <param name="dataSourceResult"></param>
        /// <returns></returns>
        Task CreateResultDataSourceAsync(DataSourceResult dataSourceResult);

        /// <summary>
        /// Create an instance of a <see cref="GameResult"/>
        /// </summary>
        /// <param name="gameResult"></param>
        Task CreateGameResultAsync(GameResult gameResult);

        /// <summary>
        /// Delete an <see cref="GameResult"/> instance
        /// </summary>
        /// <param name="gameResult"></param>
        /// <returns></returns>
        Task DeleteGameResultAsync(GameResult gameResult);

        /// <summary>
        /// Create an instance of a <see cref="DataSourceRanking"/> entity
        /// </summary>
        /// <param name="dataSourceRanking"></param>
        /// <returns></returns>
        Task CreateRankingDataSourceAsync(DataSourceRanking dataSourceRanking);

        /// <summary>
        /// Create a <see cref="TeamStat"/>
        /// </summary>
        /// <param name="stat"></param>
        /// <returns></returns>
        Task CreateTeamStatAsync(TeamStat stat);

        /// <summary>
        /// Create a <see cref="User"/>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<User> CreateUserAsync(User user, string password);

        /// <summary>
        /// Create a <see cref="UserClaim"/> for <see cref="User"/>
        /// </summary>
        /// <param name="userClaim"></param>
        /// <returns></returns>
        Task AddUserClaimAsync(UserClaim userClaim);

        //  Helpers

        /// <summary>
        /// <see cref="SubDivision"/> code already exist?
        /// </summary>
        /// <param name="subDivCode"></param>
        /// <returns></returns>
        bool SubdivisionCodeExist(string subDivCode);

        /// <summary>
        /// <see cref="Team"/> code already exist?
        /// </summary>
        /// <param name="teamCode"></param>
        /// <returns></returns>
        bool TeamCodeExist(string teamCode);
    }
}

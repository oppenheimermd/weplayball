using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WePlayBall.Data;
using WePlayBall.Models;
using WePlayBall.Models.DTO;

namespace WePlayBall.Service
{
    // ReSharper disable once InconsistentNaming
    public interface IWPBService
    {
        //  Queries

        /// <summary>
        /// Return a strong collection of <see cref="DataSourceResult"/> items;
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<DataSourceResult>> GetDataSourceResultsAsync();

        //  Persistence

        /// <summary>
        /// Save an instance of a <see cref="DataSourceResult"/> entity
        /// </summary>
        /// <param name="dataSourceResult"></param>
        /// <returns></returns>
        Task SaveDataSourceResultAsync(DataSourceResult dataSourceResult);

        /// <summary>
        /// Save an instance of a <see cref="DataSourceRanking"/> entity
        /// </summary>
        /// <param name="dataSourceRanking"></param>
        /// <returns></returns>
        Task SaveDataSourceRankingAsync(DataSourceRanking dataSourceRanking);

        /// <summary>
        /// Save an instance of a <see cref="DataSourceFixture"/> entity
        /// </summary>
        /// <param name="dataSourceFixture"></param>
        /// <returns></returns>
        Task SaveDataSourceFixtureAsync(DataSourceFixture dataSourceFixture);

        /// <summary>
        /// Save an instance of a <see cref="Division"/> entity
        /// </summary>
        /// <param name="division"></param>
        /// <returns></returns>
        Task SaveDivisionAsync(Division division);

        /// <summary>
        /// Save and instance of a <see cref="GameResult"/> entity
        /// </summary>
        /// <param name="gameResult"></param>
        /// <returns></returns>
        Task SaveMatchResult(GameResult gameResult);
    }
}

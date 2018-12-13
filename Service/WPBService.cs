using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WePlayBall.Data;
using WePlayBall.Models;
using WePlayBall.Models.DTO;
using WePlayBall.Models.Helpers;

namespace WePlayBall.Service
{
    // ReSharper disable once InconsistentNaming
    public class WPBService : IWPBService
    {
        private readonly WPBDataContext _wpbDataContext;

        public WPBService(WPBDataContext wpbDataContext)
        {
            _wpbDataContext = wpbDataContext;
        }

        //  Queries
        public async Task<IEnumerable<DataSourceResult>> GetDataSourceResultsAsync()
        {
            var result = await _wpbDataContext.DataSourceResults
                .OrderByDescending(x => x.TimeStamp)
                .AsNoTracking().ToListAsync();

            return result;
        }


        //  Persistence

        public async Task SaveDataSourceResultAsync(DataSourceResult dataSourceResult)
        {
            _wpbDataContext.DataSourceResults.Add(dataSourceResult);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task SaveDataSourceRankingAsync(DataSourceRanking dataSourceRanking)
        {
            _wpbDataContext.DataSourceRankings.Add(dataSourceRanking);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task SaveDataSourceFixtureAsync(DataSourceFixture dataSourceFixture)
        {
            _wpbDataContext.DataSourceFixtures.Add(dataSourceFixture);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task SaveDivisionAsync(Division division)
        {
            _wpbDataContext.Divisions.Add(division);
            await _wpbDataContext.SaveChangesAsync();
        }

        public async Task SaveMatchResult(GameResult gameResult)
        {
            _wpbDataContext.GameResults.Add(gameResult);
            await _wpbDataContext.SaveChangesAsync();
        }
    }
}

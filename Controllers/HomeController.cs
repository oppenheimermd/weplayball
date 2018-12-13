using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weplayball.Models;
using WePlayBall.Helpers;
using WePlayBall.Models;
using WePlayBall.Security;
using WePlayBall.Service;

namespace WePlayBall.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWPBService _wpbService;

        public HomeController(IWPBService wpbService)
        {
            _wpbService = wpbService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Helpers

        /// <summary>
        /// Method for priming the database with <see cref="DataSourceResult"/> entities
        /// </summary>
        /// <returns></returns>
        public async Task AddDataSourceResultsAsync()
        {
            var encrypter = new TyfSimpleAes();

            var dataSources = new List<DataSourceResult>()
            {
                new DataSourceResult()
                {
                    DataSourceDescription = @"First Division Results - 2017/2018",
                    Division = "Division 1",
                    DivisionCode = "DIV1",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=140",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=140"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_divisionTable']"
                },
                new DataSourceResult()
                {
                    DataSourceDescription = @"Second Division Results - 2017/2018",
                    Division = "Division 2",
                    DivisionCode = "DIV2",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=141",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=141"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_divisionTable']"
                },
                //  Below should throw an error
                new DataSourceResult()
                {
                    DataSourceDescription = @"Third Division Results - 2017/2018",
                    Division = "Division 3",
                    DivisionCode = "DIV3",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=142",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=142"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_divisionTable']"
                }
            };

            foreach (var item in dataSources)
            {
                try
                {
                    await _wpbService.SaveDataSourceResultAsync(item);
                }
                catch (Exception err)
                {
                    var msg = err.ToString();
                }
            }
        }

        /// <summary>
        /// Helper method for priming the database with <see cref="DataSourceRanking"/> entities
        /// </summary>
        /// <returns></returns>
        public async Task AddDataSourceRankingAsync()
        {
            var encrypter = new TyfSimpleAes();

            var dataSources = new List<DataSourceRanking>()
            {
                new DataSourceRanking()
                {
                    DataSourceDescription = @"First Division Rankings - 2017/2018",
                    Division = "Division 1",
                    DivisionCode = "DIV1",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=ll&LeagueId=140",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=ll&LeagueId=140"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_divisionTable']"
                },
                new DataSourceRanking()
                {
                    DataSourceDescription = @"Second Division Rankings - 2017/2018",
                    Division = "Division 2",
                    DivisionCode = "DIV2",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=ll&LeagueId=141",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=ll&LeagueId=141"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_divisionTable']"
                },
                new DataSourceRanking()
                {
                    DataSourceDescription = @"Third Division Rankings - 2017/2018",
                    Division = "Division 3",
                    DivisionCode = "DIV3",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=ll&LeagueId=142",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=ll&LeagueId=142"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_divisionTable']"
                }
            };

            foreach (var item in dataSources)
            {
                await _wpbService.SaveDataSourceRankingAsync(item);
            }
        }

        /// <summary>
        /// Helper method for priming the database with <see cref="DataSourceFixture"/> entities
        /// </summary>
        /// <returns></returns>
        public async Task AddDataSourceFixturesAsync()
        {
            var encrypter = new TyfSimpleAes();

            var dataSources = new List<DataSourceFixture>()
            {
                new DataSourceFixture()
                {
                    DataSourceDescription = @"First Division Fixtures - 2017/2018",
                    Division = "Division 1",
                    DivisionCode = "DIV1",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=140",
                    UrlHash =
                        encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=140"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_fixturesTable']"
                },
                new DataSourceFixture()
                {
                    DataSourceDescription = @"Second Division Fixtures - 2017/2018",
                    Division = "Division 2",
                    DivisionCode = "DIV2",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=141",
                    UrlHash =
                        encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=141"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_fixturesTable']"
                },
                new DataSourceFixture()
                {
                    DataSourceDescription = @"Third Division Fixtures - 2017/2018",
                    Division = "Division 3",
                    DivisionCode = "DIV3",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=142",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=142"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_fixturesTable']"
                }
            };

            foreach (var item in dataSources)
            {
                await _wpbService.SaveDataSourceFixtureAsync(item);
            }
        }

        public async Task AddDivisionsAsync()
        {

            var dataSources = new List<Division>()
            {
                new Division()
                {

                    DivisionName = "NL First Division 18/19",
                    DivisionCode = "DIV1",
                },
                new Division()
                {
                    DivisionName = "NL Second Division 18/19",
                    DivisionCode = "DIV2"
                },
                new Division()
                {
                    DivisionName = "NL Third Division 18/19",
                    DivisionCode = "DIV3",
                }
            };

            foreach (var item in dataSources)
            {
                await _wpbService.SaveDivisionAsync(item);
            }
        }

        /*public async Task GetResultsAsync()
        {
            var resultsDataSource = await _wpbService.GetDataSourceResultsAsync();

            //  loop through results data source and get results
            foreach (var src in resultsDataSource)
            {
                var matchResults = ParseDataSource.ParseResultDataSource(src, src.DivisionCode, src.ClassNameNode);
                foreach (var mr in matchResults)
                {
                    await _wpbService.SaveMatchResult(mr);
                }
            }
        }*/

        /*public async Task GetRankingsAsync()
        {
            var rankingsDataSource = await Uow.DataSourceRankings.GetAllAsync();

            //  loop though results
            foreach (var src in rankingsDataSource)
            {
                var rankingResults = ParseDataDource.ParseRankingSource(src, src.Division, src.DivisionCode, src.ClassNameNode);
                foreach (var rr in rankingResults)
                {
                    try
                    {
                        await Uow.Ranks.CreateAsync(rr);
                    }
                    catch (Exception err)
                    {
                        var msg = err.ToString();
                    }
                }
            }
        }*/

        #endregion
    }
}

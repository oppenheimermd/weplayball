using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WePlayBall.Models;
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

        public async Task<IActionResult> Index()
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


        public async Task PrimeDataSources()
        {
            await AddDataSourceFixturesAsync();
            await AddDataSourceResultsAsync();
            await AddDataSourceRankingAsync();
        }

        /// <summary>
        /// Prime the fixture data sources
        /// </summary>
        /// <returns></returns>
        public async Task AddDataSourceFixturesAsync()
        {
            var encrypter = new TyfSimpleAes();

            var dataSources = new List<DataSourceFixture>()
            {
                new DataSourceFixture()
                {
                    DataSourceDescription = @"First Division Fixtures - 18/19",
                    Division = "Division 1",
                    DivisionCode = "DIV1",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=148",
                    UrlHash =
                        encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=148"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_fixturesTable']"
                },
                new DataSourceFixture()
                {
                    DataSourceDescription = @"Second Division Fixtures - 18/19",
                    Division = "Division 2",
                    DivisionCode = "DIV2",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=149",
                    UrlHash =
                        encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=149"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_fixturesTable']"
                },
                new DataSourceFixture()
                {
                    DataSourceDescription = @"Third Division Fixtures - 18/19",
                    Division = "Division 3",
                    DivisionCode = "DIV3",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=150",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lf&LeagueId=150"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_fixturesTable']"
                }
            };

            foreach (var item in dataSources)
            {
                await _wpbService.CreateFixtureDataSourceAsync(item);
            }
        }

        public async Task AddDataSourceResultsAsync()
        {
            var encrypter = new TyfSimpleAes();
            var collection = new List<DataSourceResult>();

            var dataSources = new List<DataSourceResult>()
            {
                new DataSourceResult()
                {
                    DataSourceDescription = @"First Division Results - 18/19",
                    Division = "Division 1",
                    DivisionCode = "DIV1",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=148",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=148"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_divisionTable']"
                },
                new DataSourceResult()
                {
                    DataSourceDescription = @"Second Division Results - 18/19",
                    Division = "Division 2",
                    DivisionCode = "DIV2",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=149",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=149"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_divisionTable']"
                },
                new DataSourceResult()
                {
                    DataSourceDescription = @"Third Division Results - 18/19",
                    Division = "Division 3",
                    DivisionCode = "DIV3",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=150",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=lr&LeagueId=150"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_divisionTable']"
                }
            };

            foreach (var item in dataSources)
            {
                try
                {
                    await _wpbService.CreateResultDataSourceAsync(item);
                }
                catch (Exception err)
                {
                    var msg = err.ToString();
                }
            }
        }

        /*public async Task UpdateTeam()
        {
            var teams = await _wpbService.GetTeamsAllAdminAsync();

            foreach (var team in teams)
            {
                team.HasLogo = false;
                await _wpbService.UpdateTeamAsync(team);
            }

        }*/

        public async Task AddDataSourceRankingAsync()
        {
            var encrypter = new TyfSimpleAes();
            var collection = new List<DataSourceRanking>();

            var dataSources = new List<DataSourceRanking>()
            {
                new DataSourceRanking()
                {
                    DataSourceDescription = @"First Division Rankings - 18/19",
                    Division = "Division 1",
                    DivisionCode = "DIV1",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=ll&LeagueId=148",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=ll&LeagueId=148"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_divisionTable']"
                },
                new DataSourceRanking()
                {
                    DataSourceDescription = @"Second Division Rankings - 18/19",
                    Division = "Division 2",
                    DivisionCode = "DIV2",
                    Url = @"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=ll&LeagueId=149",
                    UrlHash = encrypter.Encrypt(@"http://www.gbwba.org.uk/gbwba/index.cfm/the-league/?v=ll&LeagueId=149"),
                    TimeStamp = SystemTime.Now(),
                    ClassNameNode = @"//table[@class='leagueManager_divisionTable']"
                },
                new DataSourceRanking()
                {
                    DataSourceDescription = @"Third Division Rankings - 18/19",
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
                try
                {
                    await _wpbService.CreateRankingDataSourceAsync(item);
                }
                catch (Exception err)
                {
                    var msg = err.ToString();
                }
            }
        }

        #endregion
    }
}

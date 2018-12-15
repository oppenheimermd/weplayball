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

        public async Task<IActionResult> Index()
        {
            //  ***Run
            //await AddDataSourceFixturesAsync();
            //await AddDataSourceResultsAsync();
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

        #endregion
    }
}

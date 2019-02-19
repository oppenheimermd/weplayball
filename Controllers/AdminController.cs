using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WePlayBall.Authorization;
using WePlayBall.Helpers;
using WePlayBall.Models;
using WePlayBall.Models.Helpers;
using WePlayBall.Security;
using WePlayBall.Service;
using WePlayBall.Settings;

namespace WePlayBall.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWPBService _wpbService;
        private readonly SiteConfig _siteSettings;

        public AdminController(IWPBService wpbService, SiteConfig siteSettings)
        {
            _wpbService = wpbService;
            _siteSettings = siteSettings;
        }

        [Authorize(Policy = WpbPolicy.PolicyReadEditTeamsAll)]
        public async Task<IActionResult> Index(int? page)
        {

            //await GetFixturesDataAsync();
            //await GetMatchResultsAsync();
            //await GetTeamStatsResultsAsync();

            /*var gameResults = await _wpbService.GetGameResultsAsync();
            foreach (var game in gameResults)
            {
                await _wpbService.DeleteGameResultAsync(game);
            }*/
            //await GetRankResultsAsync();

            return View();
        }

        //  Division

        public IActionResult DivisionsAll(int? page)
        {
            var divisions = _wpbService.GetDivisionsPageable(page);
            return View(divisions);
        }

        public async Task<IActionResult> DivisionDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await _wpbService.GetDivisionAsync(id);
            if (division == null)
            {
                return NotFound();
            }

            return View(division);
        }

        public IActionResult DivisionCreate()
        {
            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DivisionCreate([Bind("Id,DivisionName,DivisionCode")] Division division)
        {
            if (!ModelState.IsValid) return View(division);

            division.DivisionCode = division.DivisionCode.ToUpper();

            //  Check that code is unique
            await _wpbService.CreateDivisionAsync(division);
            return RedirectToAction(nameof(DivisionsAll));
        }

        // GET: Admin/DivisionEdit/5
        public async Task<IActionResult> DivisionEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var divisions = await _wpbService.GetDivisionAsync(id.Value);
            if (divisions == null)
            {
                return NotFound();
            }

            return View(divisions);
        }

        // POST: Admin/DivisionEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DivisionEdit(int id, [Bind("Id,DivisionName,DivisionCode")] Division division)
        {
            if (id != division.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(division);
            var editSuccess = await _wpbService.UpdateDivisionAsync(division);
            if (editSuccess)
            {
                return RedirectToAction(nameof(DivisionsAll));
            }

            return NotFound();
        }

        //  Sub division

        public IActionResult SubDivisionsAll(int? page)
        {
            var subdivisions = _wpbService.GetSubDivisionsPageable(page);
            return View(subdivisions);
        }

        public async Task<IActionResult> SubDivisionDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subDivision = await _wpbService.GetSubDivisionAsync(id);
            if (subDivision == null)
            {
                return NotFound();
            }

            return View(subDivision);
        }

        public async Task<IActionResult> SubDivisionCreate()
        {
            var divisionList = await _wpbService.GetDivisionDropListAsync();

            ViewData["Division"] = new SelectList(divisionList, "Id", "DivisionName");
            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubDivisionCreate([Bind("Id,SubDivisionTitle,SubDivisionCode,DivisionId")]
            SubDivision subDivision)
        {
            if (!ModelState.IsValid)
            {
                var divisionList = await _wpbService.GetDivisionDropListAsync();
                ViewData["Division"] = new SelectList(divisionList, "Id", "DivisionName");
                return View(subDivision);
            }

            subDivision.SubDivisionCode = subDivision.SubDivisionCode.ToUpper();

            var subDivCodeExist = _wpbService.SubdivisionCodeExist(subDivision.SubDivisionCode);
            if (subDivCodeExist)
            {
                var divisionList = await _wpbService.GetDivisionDropListAsync();
                ViewData["Division"] = new SelectList(divisionList, "Id", "DivisionName");
                ModelState.AddModelError("", "$Subdivision code {subDivision.SubDivisionCode} is already in use.");
                return View(subDivision);
            }


            await _wpbService.CreateSubDivisionAsync(subDivision);
            return RedirectToAction(nameof(SubDivisionsAll));
        }

        // GET: Admin/SubDivisionEdit/5
        public async Task<IActionResult> SubDivisionEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var divisions = await _wpbService.GetSubDivisionAsync(id.Value);
            if (divisions == null)
            {
                return NotFound();
            }

            var divisionList = await _wpbService.GetDivisionDropListAsync();
            ViewData["Division"] = new SelectList(divisionList, "Id", "DivisionName");
            return View(divisions);
        }

        // POST: Admin/SubDivisionEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubDivisionEdit(int id,
            [Bind("Id,SubDivisionTitle,SubDivisionCode,DivisionId")]
            SubDivision subDivision)
        {
            if (id != subDivision.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid) return View(subDivision);
            var editSuccess = await _wpbService.UpdateSubDivisionAsync(subDivision);
            if (editSuccess)
            {
                return RedirectToAction(nameof(SubDivisionsAll));
            }

            return NotFound();
        }

        //  Teams

        public IActionResult TeamsAll(int? page)
        {
            var teams = _wpbService.GetTeamsPageable(page);
            return View(teams);
        }

        public async Task<IActionResult> TeamDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _wpbService.GetTeamAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        public async Task<IActionResult> TeamCreate()
        {
            var divisionList = await _wpbService.GetSubDivisionDropListAsync();
            ViewData["SubDivision"] = new SelectList(divisionList, "Id", "SubDivisionTitle");
            return View();
        }

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamCreate([Bind("Id,TeamName,TeamCode,SubDivisionId")]
            Team team)
        {
            if (!ModelState.IsValid)
            {
                var divisionList = await _wpbService.GetSubDivisionDropListAsync();
                ViewData["SubDivision"] = new SelectList(divisionList, "Id", "SubDivisionTitle");
                return View(team);
            }

            team.TeamCode = team.TeamCode.ToUpper();

            var subDivCodeExist = _wpbService.TeamCodeExist(team.TeamCode);
            if (subDivCodeExist)
            {
                var divisionList = await _wpbService.GetSubDivisionDropListAsync();
                ViewData["SubDivision"] = new SelectList(divisionList, "Id", "SubDivisionTitle");
                ModelState.AddModelError("", "$Team code: {team.TeamCode} is already in use.");
                return View(team);
            }


            await _wpbService.CreateTeamAsync(team);
            return RedirectToAction(nameof(TeamsAll));
        }

        // GET: Admin/TeamEditEdit/5
        public async Task<IActionResult> TeamEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _wpbService.GetTeamAsync(id.Value);
            if (team == null)
            {
                return NotFound();
            }

            var divisionList = await _wpbService.GetSubDivisionDropListAsync();
            ViewData["SubDivision"] = new SelectList(divisionList, "Id", "SubDivisionTitle");
            return View(team);
        }

        // POST: Admin/TeamEdit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeamEdit(int id, [Bind("Id,TeamName,TeamCode,SubDivisionId, Address,PostCode,Website")]
            Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            //  once season is in season is in session no changes to team code
            //team.TeamCode = team.TeamCode.ToUpper();

            if (!ModelState.IsValid)
            {
                var divisionList = await _wpbService.GetSubDivisionDropListAsync();
                ViewData["SubDivision"] = new SelectList(divisionList, "Id", "SubDivisionTitle");
                return View(team);
            }

            //  once season is in season is in session no changes to team code
            /*var subDivCodeExist = _wpbService.TeamCodeExist(team.TeamCode);
            if (subDivCodeExist)
            {
                var divisionList = await _wpbService.GetSubDivisionDropListAsync();
                ViewData["SubDivision"] = new SelectList(divisionList, "Id", "SubDivisionTitle");
                ModelState.AddModelError("", "$Team code: {team.TeamCode} is already in use.");
                return View(team);
            }*/

            var teamCodeSanityCheck = await _wpbService.GetTeamAsync(id);
            if (teamCodeSanityCheck != null)
            {
                // no changes to team code 
                if (team.TeamCode != teamCodeSanityCheck.TeamCode)
                {
                    var divisionList = await _wpbService.GetSubDivisionDropListAsync();
                    ViewData["SubDivision"] = new SelectList(divisionList, "Id", "SubDivisionTitle");
                    ModelState.AddModelError("", "Changes to team code not allow while in session!");
                    return View(team);
                }
            }
            else
            {
                return NotFound();
            }

            var editSuccess = await _wpbService.UpdateTeamAsync(team);
            if (editSuccess)
            {
                return RedirectToAction(nameof(TeamsAll));
            }

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> TeamsBySubDivision(int? page, string Id)
        {
            var subDivId = int.Parse(Id);
            var pageNumber = page ?? 1;
            var subDivision = await _wpbService.GetSubDivisionAsync(subDivId);
            var teams = _wpbService.GetTeamsBySubDivisionPageable(pageNumber, subDivId);

            ViewData["SubDivisionId"] = subDivision.Id;
            ViewData["SubDivisionName"] = subDivision.SubDivisionTitle;
            return View(teams);
        }

        //  Fixture Datasource

        public IActionResult FixtureAll(int? page)
        {
            var fixtures = _wpbService.GetFixturePageable(page);
            return View(fixtures);
        }

        public async Task<IActionResult> ResultsAll(int? page)
        {
            // last stat report run
            ViewData["LastReportRun"] = await _wpbService.GetLastResultsReportRun();
            var results = _wpbService.GetGameResultsPageable(page);
            return View(results);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateResultsAsync()
        {
            await GetMatchResultsAsync();
            return RedirectToAction(nameof(ResultsAll));
        }

        [HttpGet]
        public async Task<IActionResult> AddTeamLogo(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _wpbService.GetTeamAsync(id.Value);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Burgers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTeamLogo([Bind("Id,LogoImage")] Team team)
        {

            var teamExist = await _wpbService.GetTeamAsync(team.Id);
            if (teamExist == null)
            {
                return View();
            }

            //  Picture required
            if (HttpContext.Request.Form.Files.Count != 0)
            {

                var teamLogo = HttpContext.Request.Form.Files[0];
                //  yes
                if (teamLogo.Length > 0)
                {
                    //  filename format: teamcode-logo (to lower)
                    var newFilename = teamExist.TeamCode + "-" + "logo";
                    var fileName = await _wpbService.SaveTeamLogoAsync(teamLogo, newFilename.ToLower());
                    teamExist.HasLogo = true;
                    teamExist.Logo = fileName;
                    await _wpbService.UpdateTeamAsync(teamExist);

                    return RedirectToAction(nameof(TeamsAll));
                }
                else
                {
                    ModelState.AddModelError("", "Image missing");
                    return View();
                }

            }
            else
            {
                ModelState.AddModelError("", "Image required");
                return View();
            }
        }

        //  Run once only
        //  Run on 17/12/2018
        /*public async Task AddDivisionsAsync()
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
                await _wpbService.CreateDivisionAsync(item);
            }
        }

        /// <summary>
        /// Only need to run this function once
        /// RUN COMPLETE
        /// </summary>
        /// <returns></returns>
        public async Task GetFixturesDataAsync()
        {
            //  Get all the fixtures dataSources
            var fixturesDataSource = await _wpbService.GetFixtureDataSources();

            foreach (var src in fixturesDataSource)
            {
                var fixturesResults =
                    ParseDataSource.ParseFixturesDataSource(src, src.ClassNameNode);

                foreach (var ftr in fixturesResults)
                {
                    try
                    {
                        //  get Home team details
                        var teamHome = await _wpbService.GetTeamByTeamName(ftr.FixtureHomeTeamName);
                        var teamAway = await _wpbService.GetTeamByTeamName(ftr.FixtureAwayTeamName);

                        if (teamHome != null && teamAway != null)
                        {
                            var newFixture = new Fixture()
                            {
                                FixtureDate = ftr.FixtureDate,
                                HomeTeamId = teamHome.Id,
                                HomeTeamName = teamHome.TeamName,
                                HomeTeamCode = teamHome.TeamCode,
                                AwayTeamId = teamAway.Id,
                                AwayTeamName = teamAway.TeamName,
                                AwayTeamCode = teamAway.TeamCode,
                                //  already have the subdivision in both Home or Away team
                                SubDivisionId = teamHome.SubDivisionId
                            };

                            //  Check for null values!!
                            await _wpbService.CreateFixtureAsync(newFixture);
                        }
                        else
                        {
                            throw new System.ArgumentException(
                                "Detected null values in either teamHome, teamAway or subDivision variable!");
                        }
                    }
                    catch (Exception err)
                    {
                        var msg = err.ToString();
                    }
                }
            }

        }
        */

        public async Task GetMatchResultsAsync()
        {
            //  Get all the data sources for results
            var resultsDataSource = await _wpbService.GetAllResultDataSourceAsync();

            foreach (var result in resultsDataSource)
            {
                var matchResults =
                    ParseDataSource.ParseResultDataSource(result, result.ClassNameNode);

                foreach (var game in matchResults)
                {
                    try
                    {
                        var teamHome = await _wpbService.GetTeamByTeamName(game.HomeTeamName);
                        var teamAway = await _wpbService.GetTeamByTeamName(game.AwayTeamName);

                        if (teamHome != null && teamAway != null)
                        {
                            var winningTeam = (game.WinningTeamName == teamHome.TeamName) ? teamHome : teamAway;
                            var encodedResult = EncodeGameResult(teamHome.TeamName, teamAway.TeamName, result.TimeStamp);

                            var newResult = new GameResult()
                            {
                                TimeStamp = game.TimeStamp,
                                HomeTeamId = teamHome.Id,
                                HomeTeamName = teamHome.TeamName,
                                HomeTeamCode = teamHome.TeamCode,
                                AwayTeamId = teamAway.Id,
                                AwayTeamName = teamAway.TeamName,
                                AwayTeamCode = teamAway.TeamCode,
                                Score = game.Score,
                                WinningTeamName = winningTeam.TeamName,
                                WinningTeamCode = winningTeam.TeamCode,
                                //  already have the subdivision in both Home or Away team
                                SubDivisionId = teamHome.SubDivisionId,
                                EncodedResult = encodedResult
                            };

                            var resultExist = await _wpbService.GameResultExistAsync(newResult.EncodedResult);
                            //  only add if not in db
                            if (resultExist == false)
                            {
                                await _wpbService.CreateGameResultAsync(newResult);
                            }
                        }
                        else
                        {
                            throw new System.ArgumentException(
                                "Detected null values in either teamHome, teamAway or subDivision variable!");
                        }
                    }
                    catch (Exception err)
                    {
                        var msg = err.ToString();
                    }
                }
            }

            //  add new report tracker object
            var newReportRun = new ReportTracker()
            {
                ReportTypeCode = ModelHelpers.REPORT_RSLT
            };
            await _wpbService.CreateReportHistory(newReportRun);

        }

        //  Team Stats

        [HttpGet]
        public async Task<IActionResult> StatsBySubDivision(int? page)
        {
            var subDivsAll = _wpbService.GetSubDivisionsPageable(page);
            // last stat report run
            ViewData["LastReportRun"] = await _wpbService.GetLastStatReportRun();

            return View(subDivsAll);
        }


        [HttpGet]
        public async Task<IActionResult> UpdateStatsReport()
        {
            await RunLeagueReportAsync();
            return RedirectToAction(nameof(StatsBySubDivision));
        }

        public async Task<List<TeamStat>> GetTeamStatsResultsAsync()
        {
            //  Get all the data sources for ranking data
            var rankingDataSource = await _wpbService.GetRankingDataSources();
            var statData = new List<TeamStat>();

            foreach (var result in rankingDataSource)
            {
                var rankStat = ParseDataSource.ParseRankingSource(result, result.ClassNameNode);
                foreach (var stat in rankStat)
                {
                    try
                    {
                        var team = await _wpbService.GetTeamByTeamName(stat.TeamName);
                        if (team != null)
                        {

                            var newStat = new TeamStat()
                            {
                                TeamName = team.TeamName,
                                TeamId = team.Id,
                                TeamCode = team.TeamCode,
                                SubDivisionId = team.SubDivisionId,
                                Position = stat.Position,
                                GamesPlayed = stat.GamesPlayed,
                                GamesWon = stat.GamesWon,
                                GamesLost = stat.GamesLost,
                                BasketsFor = stat.BasketsFor,
                                BasketsAganist = stat.BasketsAganist,
                                PointsDifference = stat.PointsDifference,
                                Points = stat.Points,
                                WinPercentage = StatisticsCalculator.WinPercentage(stat.GamesWon, stat.GamesPlayed).ToString(),
                                LossPercentage = StatisticsCalculator.LossPercentage(stat.GamesLost, stat.GamesPlayed).ToString(),
                                BasketsPerGame = StatisticsCalculator.BasketsPerGame(stat.BasketsFor, stat.GamesPlayed).ToString(),
                                WinLossPercent = StatisticsCalculator.WinLossPercentage(stat.GamesWon, stat.GamesLost).ToString(),
                                WinsOver500 = StatisticsCalculator.WinsOver50(stat.GamesWon, stat.GamesLost).ToString(),
                                WPyth = StatisticsCalculator.WPyth(stat.BasketsFor, stat.BasketsAganist).ToString(),
                            };
                            statData.Add(newStat);
                        }
                        else
                        {
                            throw new System.ArgumentException(
                                "Detected null values in either team variable!");
                        }

                    }
                    catch (Exception err)
                    {
                        var msg = err.ToString();
                    }
                }
            }

            return statData;

        }

        [HttpGet]
        public IActionResult ViewStatsBySubDivision(int subDivId)
        {
            var stats = _wpbService.GetTeamsStatsBySubDivisionAsync(subDivId);
            return View(stats);
        }

        [HttpGet]
        public IActionResult AddInstagramItem()
        {
            var newItem = new InstagramItem();
            return View(newItem);
        }

        // POST: Burgers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddInstagramItem([Bind("Id,Date,UserId,Url,IsVideo")] InstagramItem item)
        {            
            //   try parse date
            try
            {
                //item.Date = DateTime.Parse(item.Date.ToString());
                item.Date = DateTime.ParseExact(item.Date.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (FormatException exp)
            {
                var exception = exp.ToString();
                ModelState.AddModelError("", "Date in format: DD/MM/YYYY missing");
                return View(item);
            }

            if (!ModelState.IsValid) return View(item);
            var photoUnique = await _wpbService.InstagramPhotoUnique(item.Url);
            if (!photoUnique)
            {
                ModelState.AddModelError("", $"The photo with link: {item.Url} already added.");
                return View(item);
            }


            //  Picture required
                if (HttpContext.Request.Form.Files.Count != 0)
            {

                var photo = HttpContext.Request.Form.Files[0];
                //  yes
                if (photo.Length > 0)
                {
                    var fileName = await _wpbService.SaveInstagramPhotooAsync(photo, item.IsVideo);
                    item.Filename = fileName;
                    await _wpbService.CreateInstagramItemAsync(item);
                    return RedirectToAction(nameof(InstagramFavsAll));
                }
                else
                {
                    ModelState.AddModelError("", "Image missing");
                    return View();
                }

            }
            else
            {
                ModelState.AddModelError("", "Image required");
                return View();
            }
        }

        public IActionResult InstagramFavsAll(int? page)
        {
            // last stat report run
            var results = _wpbService.GetInstgramFavsPageable(page);
            return View(results);
        }

        public async Task<IActionResult> DeleteInstagramItem(int? id)
        {
            if(!id.HasValue)
                return NotFound();

            var item = await _wpbService.GetInstagramItemAsync(id.Value);
            if(item != null)
            {
                await _wpbService.DeleteInstagramItemtAsync(item);
                return RedirectToAction(nameof(InstagramFavsAll));
            }
            else
            {
                return NotFound();
            }
        }



        public async Task PurgeStatTable(IEnumerable<TeamStat> oldTeamStats)
        {
            if (oldTeamStats == null) return;
            foreach (var stat in oldTeamStats)
            {
                await _wpbService.DeleteTeamStatAsync(stat);
            }
        }

        public async Task UpdateStatTable(IEnumerable<TeamStat> teamStats)
        {
            if (teamStats == null) return;
            foreach (var stat in teamStats)
            {
                await _wpbService.CreateTeamStatAsync(stat);
            }
        }

        public async Task RunLeagueReportAsync()
        {
            //return RedirectToAction(nameof(StatsBySubDivision));
            var staleState = await _wpbService.GetStatsAllAsync();
            var freshState = await GetTeamStatsResultsAsync();
            //  purge old team stats
            await PurgeStatTable(staleState);

            await UpdateStatTable(freshState);

            //  add new report tracker object
            var newReportRun = new ReportTracker()
            {
                ReportTypeCode = ModelHelpers.REPORT_STAT
            };
            await _wpbService.CreateReportHistory(newReportRun);

        }

        public string EncodeGameResult(string homeTeam, string awayTeam, DateTime timestamp)
        {
            return homeTeam.Trim().ToLower() + "_" + awayTeam.Trim().ToLower() + "_" + timestamp.ToLongDateString();
        }

    }
}

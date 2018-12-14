using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WePlayBall.Models;
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

        public async Task<IActionResult> Index(int? page)
        {
            //  tested for div 2
            //await GetFixturesDataAsync();
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
        public async Task<IActionResult> TeamEdit(int id, [Bind("Id,TeamName,TeamCode,SubDivisionId")]
            Team team)
        {
            if (id != team.Id)
            {
                return NotFound();
            }

            team.TeamCode = team.TeamCode.ToUpper();

            if (!ModelState.IsValid)
            {
                var divisionList = await _wpbService.GetSubDivisionDropListAsync();
                ViewData["SubDivision"] = new SelectList(divisionList, "Id", "SubDivisionTitle");
                return View(team);
            }

            var subDivCodeExist = _wpbService.TeamCodeExist(team.TeamCode);
            if (subDivCodeExist)
            {
                var divisionList = await _wpbService.GetSubDivisionDropListAsync();
                ViewData["SubDivision"] = new SelectList(divisionList, "Id", "SubDivisionTitle");
                ModelState.AddModelError("", "$Team code: {team.TeamCode} is already in use.");
                return View(team);
            }

            var editSuccess = await _wpbService.UpdateTeamAsync(team);
            if (editSuccess)
            {
                return RedirectToAction(nameof(TeamsAll));
            }

            return NotFound();
        }

        //  Fixture Datasource

        public IActionResult FixtureAll(int? page)
        {
            var fixtures = _wpbService.GetFixturePageable(page);
            return View(fixtures);
        }

        /// <summary>
        /// Only need to run this function once
        /// </summary>
        /// <returns></returns>
        public async Task GetFixturesDataAsync()
        {
            //  We're testing here, so get the one and only fixture datasouce in the db currenlty (div2 id: 2)
            var fixturesDataSource = await _wpbService.GetFixtureDataSource(2);

            var fixturesResults = ParseDataSource.ParseFixturesDataSource(fixturesDataSource, fixturesDataSource.ClassNameNode);

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
                        throw new System.ArgumentException("Detected null values in either teamHome, teamAway or subDivision variable!");
                    }
                }
                catch (Exception err)
                {
                    var msg = err.ToString();
                }
            }


        }
    }
}

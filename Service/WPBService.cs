﻿using System;
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

        public async Task<DataSourceFixture> GetFixtureDataSource(int? id)
        {
            var dataSource = await _wpbDataContext.DataSourceFixtures
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id.Value);
            return dataSource;
        }

        public async Task<Team> GetTeamByTeamName(string teamName)
        {
            var team = await _wpbDataContext.Teams
                .Where( x => x.TeamName == teamName)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return team;
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
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using WePlayBall.Models.DTO;

namespace WePlayBall.Models.Helpers
{
    public static class ModelHelpers
    {
        public static string REPORT_STAT = "STRO";

        //  Four letter code for report types
        public static List<SelectListItem> ReportTypeDropDown = new List<SelectListItem>
        {
            new SelectListItem {Text = "Stat Report", Value = "STRP"},
            new SelectListItem {Text = "Other", Value = "OTH"}
        };

        /// <summary>
        /// From <see cref="Team"/> to <see cref="TeamDto"/>
        /// </summary>
        public static readonly Expression<Func<Team, TeamDto>> AsTeamDto =
            x => new TeamDto
            {
                TeamName = x.TeamName,
                TeamCode = x.TeamCode,
                DivisionName = x.SubDivision.Division.DivisionName,
                DivisionCode = x.SubDivision.Division.DivisionCode,
                SubDivisionTitle = x.SubDivision.SubDivisionTitle,
                SubDivisionCode = x.SubDivision.SubDivisionCode
            };

        public static readonly Expression<Func<Fixture, FixturesDto>> AsFixtureDto =
            x => new FixturesDto()
            {
                FixtureDate = x.FixtureDate,
                HomeTeamName = x.HomeTeamName,
                HomeTeamCode = x.HomeTeamCode,
                AwayTeamName = x.AwayTeamName,
                AwayTeamCode = x.AwayTeamCode,
                Division = x.SubDivision.Division.DivisionName,
                DivisionCode = x.SubDivision.Division.DivisionCode,
                SubDivision = x.SubDivision.SubDivisionTitle,
                SubDivisionCode = x.SubDivision.SubDivisionCode
            };

        public static readonly Expression<Func<TeamStat, TeamStatDto>> AsTeamStatDto =
            x => new TeamStatDto()
            {
                Id = x.Id,
                TeamName = x.TeamName,
                TeamId = x.TeamId,
                TeamCode = x.TeamCode,
                SubDivisionId = x.SubDivisionId,
                Position = x.Position,
                GamesPlayed = x.GamesPlayed,
                GamesWon = x.GamesWon,
                GamesLost = x.GamesLost,
                BasketsFor = x.BasketsFor,
                BasketsAganist = x.BasketsAganist,
                PointsDifference = x.PointsDifference,
                Points = x.Points,
                WPyth = x.WPyth,
                WinsOver500 = x.WinsOver500,
                WinLossPercent = x.WinLossPercent,
                BasketsPerGame = x.BasketsPerGame,
                LossPercentage = x.LossPercentage,
                WinPercentage = x.WinPercentage
            };

        // Typed lambda expression for Select() method. 
        /*public static readonly Expression<Func<Division, DivisonDto>> AsDivisionDto =
            x => new DivisonDto
            {
                DivisionName = x.DivisionName,
                DivisionCode = x.DivisionCode
            };

        public static readonly Expression<Func<GameResult, GameResultDto>> AsGameResultDto =
            x => new GameResultDto
            {
                TimeStamp = x.TimeStamp,
                HomeTeamName = x.HomeTeam.TeamName,
                HomeTeamCode = x.HomeTeam.TeamCode,
                SubDivisionId = x.HomeTeam.SubDivision.Id,
                SubDivisionTitle = x.HomeTeam.SubDivision.SubDivisionTitle,
                SubDivisionCode = x.HomeTeam.SubDivision.SubDivisionCode,
                DivisionId = x.HomeTeam.SubDivision.Division.Id,
                DivisionTitle = x.HomeTeam.SubDivision.Division.DivisionName,
                DivisionCode = x.HomeTeam.SubDivision.Division.DivisionCode,
                Score = x.Score,
                WinnerTeamName = x.Winner.TeamName,
                WinnerTeamCode = x.Winner.TeamCode
            };*/

        public static readonly Expression<Func<SubDivision, SubDivisionDto>> AsSubDivisionDto =
            x => new SubDivisionDto()
            {
                Id = x.Id,
                SubDivisionTitle = x.SubDivisionTitle,
                SubDivisionCode = x.SubDivisionCode,
                DivisionId = x.Division.Id,
                DivisionName = x.Division.DivisionName,
                DivisionCode = x.Division.DivisionCode
            };

        public static readonly Expression<Func<DataSourceResult, ResultDataSourceDto>> AsResultDataSourceDto =
            x => new ResultDataSourceDto()
            {
                Id = x.Id,
                DataSourceDescription = x.DataSourceDescription,
                Url = x.Url,
                Division = x.Division,
                GameDivisionCode = x.DivisionCode,
                UrlHash = x.UrlHash,
                TimeStamp = x.TimeStamp
            };
    }
}
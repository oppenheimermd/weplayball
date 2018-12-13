using System;
using System.Linq.Expressions;
using WePlayBall.Models.DTO;

namespace WePlayBall.Models.Helpers
{
    public static class ModelHelpers
    {
        // Typed lambda expression for Select() method. 
        public static readonly Expression<Func<Division, DivisonDto>> AsDivisionDto =
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
            };

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
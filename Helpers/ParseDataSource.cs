using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HtmlAgilityPack;
using WePlayBall.Models;
using WePlayBall.Models.DTO;
using WePlayBall.Security;

namespace WePlayBall.Service
{
    public static class ParseDataSource
    {
        /// <summary>
        /// Parse Document and return array of results.
        /// </summary>
        /// <param name="htmlString"></param>
        /// <param name="classnameNode"></param>
        /// <returns></returns>
        private static IEnumerable<string> ParseHtmlSplitTables(string htmlString, string classnameNode)
        {
            var result = new string[] { };

            if (String.IsNullOrWhiteSpace(htmlString)) return result;
            var doc = new HtmlDocument();
            doc.LoadHtml(htmlString);

            var tableNodes = doc.DocumentNode.SelectNodes(classnameNode);
            if (tableNodes != null)
            {
                result = Array.ConvertAll(tableNodes.ToArray(), n => n.OuterHtml);
            }

            return result;
        }

        private static DateTime GetDate(string date)
        {
            //  format:  23-September-2017
            var dateArray = date.Split('-');
            var dateAsString = dateArray[0] + " " + dateArray[1] + " " + dateArray[2];
            var timeStamp = DateTime.Parse(dateAsString);
            return timeStamp;
        }

        /// <summary>
        /// Get wining team name in format: "Norwich Lowriders"
        /// </summary>
        /// <param name="resultTem"></param>
        private static void GetWinner(GameResultParseDto resultTem)
        {
            var scoreArray = resultTem.Score.Split(null);
            var homeScore = int.Parse(scoreArray[0]);
            var awayScore = int.Parse(scoreArray[2]);
            resultTem.WinningTeamName = homeScore > awayScore ? resultTem.HomeTeamName : resultTem.AwayTeamName;
        }

        //  Tested
        //  14/12/2018
        public static IEnumerable<FixtureResultParseDto> ParseFixturesDataSource(DataSourceFixture source, string nodeClassname)
        {
            var results = new List<FixtureResultParseDto>();
            var web = new HtmlWeb();
            var htmlDoc = web.Load(source.Url);

            var htmlTables = ParseHtmlSplitTables(htmlDoc.ParsedText, nodeClassname);


            foreach (var table in htmlTables)
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(table);
                //Gives you all the TR
                var trs = htmlDocument.DocumentNode.Descendants("TR").ToArray();

                var takeCount = (trs.Length - 2);
                var trsFiltered = trs.Skip(2).Take(takeCount);
                var trDivisionDetail = trs.First();
                var subDivisionDetails = trDivisionDetail.InnerText.Trim();

                foreach (var tr in trsFiltered)
                {
                    // Get All the TD's
                    var tds = tr.Descendants("TD").ToArray();


                    var rslt = new FixtureResultParseDto
                    {
                        FixtureDate = GetDate(tds[0].InnerText.Trim()),
                        FixtureHomeTeamName = tds[1].InnerText.Trim(),
                        FixtureAwayTeamName = tds[2].InnerText.Trim(),
                        SubDivisionParseName = subDivisionDetails
                    };

                    results.Add(rslt);
                }
            }

            return results;
        }

        //  Tested
        //  15/12/2018
        public static IEnumerable<GameResultParseDto> ParseResultDataSource(DataSourceResult source, string nodeClassname)
        {
            var results = new List<GameResultParseDto>();
            var web = new HtmlWeb();
            var htmlDoc = web.Load(source.Url);

            var htmlTables = ParseHtmlSplitTables(htmlDoc.ParsedText, nodeClassname);


            foreach (var table in htmlTables)
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(table);
                //Gives you all the TR
                var trs = htmlDocument.DocumentNode.Descendants("TR").ToArray();

                var takeCount = (trs.Length - 2);
                var trsFiltered = trs.Skip(2).Take(takeCount);
                var trDivisionDetail = trs.First();
                var subDivisionDetails = trDivisionDetail.InnerText.Trim();

                foreach (var tr in trsFiltered)
                {
                    // Get All the TD's
                    var tds = tr.Descendants("TD").ToArray();


                    var rslt = new GameResultParseDto
                    {
                        TimeStamp = GetDate(tds[0].InnerText.Trim()),
                        HomeTeamName = tds[1].InnerText.Trim(),
                        Score = tds[2].InnerText.Trim(),
                        AwayTeamName = tds[3].InnerText.Trim(),
                        SubDivisionParseName = subDivisionDetails
                    };

                    GetWinner(rslt);
                    results.Add(rslt);
                }
            }

            return results;
        }

        public static IEnumerable<RankResultParseDto> ParseRankingSource(DataSourceRanking source, string nodeClassname)
        {
            var results = new List<RankResultParseDto>();
            var web = new HtmlWeb();
            var htmlDoc = web.Load(source.Url);

            var htmlTables = ParseHtmlSplitTables(htmlDoc.ParsedText, nodeClassname);


            foreach (var table in htmlTables)
            {
                var htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(table);
                //Gives you all the TR
                var trs = htmlDocument.DocumentNode.Descendants("TR").ToArray();

                var takeCount = (trs.Length - 2);
                var trsFiltered = trs.Skip(2).Take(takeCount);
                var trDivisionDetail = trs.First();
                var subDivisionDetails = trDivisionDetail.InnerText.Trim();

                foreach (var tr in trsFiltered)
                {
                    // Get All the TD's
                    var tds = tr.Descendants("TD").ToArray();


                    var rslt = new RankResultParseDto
                    {
                        Position = int.Parse((tds[0].InnerText.Trim())),
                        TeamName = tds[1].InnerText.Trim(),
                        GamesPlayed = int.Parse(tds[2].InnerText.Trim()),
                        GamesWon = int.Parse(tds[3].InnerText.Trim()),
                        GamesLost = int.Parse(tds[5].InnerText.Trim()),
                        BasketsFor = int.Parse(tds[6].InnerText.Trim()),
                        BasketsAganist = int.Parse(tds[7].InnerText.Trim()),
                        PointsDifference = int.Parse(tds[8].InnerText.Trim()),
                        Points = int.Parse(tds[10].InnerText.Trim())
                    };

                    results.Add(rslt);
                }
            }

            return results;
        }
    }
}

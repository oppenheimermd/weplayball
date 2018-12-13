using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using HtmlAgilityPack;
using WePlayBall.Models;
using WePlayBall.Models.DTO;
using WePlayBall.Security;

namespace WePlayBall.Helpers
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
        /// Get sub-division code in format: NL Third Division North 17/18 Results
        /// </summary>
        /// <param name="subDivision"></param>
        /// <returns></returns>
        private static string GetSubDivisionCodeResults(string subDivision)
        {
            string subDivisionCode;

            switch (subDivision)
            {
                //  FirstDivision
                case @"NL First Division North 17/18 Results":
                    subDivisionCode = @"FDNH";
                    break;
                case @"NL First Division South 17/18 Results":
                    subDivisionCode = @"FDSH";
                    break;
                //  Second Division
                case @"NL Second Division North 17/18 Results":
                    subDivisionCode = @"SDNH";
                    break;
                case @"NL Second Division South 17/18 Results":
                    subDivisionCode = @"SDSH";
                    break;
                //  Third Division
                case @"NL Third Division North 17/18 Results":
                    subDivisionCode = @"TDNH";
                    break;
                case @"NL Third Division Central 17/18 Results":
                    subDivisionCode = @"TDCL";
                    break;
                case @"NL Third Division 17/18 South West Results":
                    subDivisionCode = @"TDSW";
                    break;
                case @"NL Third Division 17/18 South East Results":
                    subDivisionCode = @"TDSE";
                    break;
                /*case @"":
                    subDivisionCode = @"";
                    break;
                case @"":
                    subDivisionCode = @"";
                    break;
                case @"":
                    subDivisionCode = @"";
                    break;*/
                default:
                    throw new InvalidEnumArgumentException();
            }

            return subDivisionCode;
        }

        // <summary>
        /// Get sub-division code in format: NL First Division North 17/18
        /// </summary>
        /// <param name="subDivision"></param>
        /// <returns></returns>
        private static string GetSubDivisionCode(string subDivision)
        {
            string subDivisionCode;

            switch (subDivision)
            {
                //  FirstDivision
                case @"NL First Division North 17/18":
                    subDivisionCode = @"FDNH";
                    break;
                case @"NL First Division South 17/18":
                    subDivisionCode = @"FDSH";
                    break;
                //  Second Division
                case @"NL Second Division North 17/18":
                    subDivisionCode = @"SDNH";
                    break;
                case @"NL Second Division South 17/18":
                    subDivisionCode = @"SDSH";
                    break;
                //  Third Division
                case @"NL Third Division North 17/18":
                    subDivisionCode = @"TDNH";
                    break;
                case @"NL Third Division Central 17/18":
                    subDivisionCode = @"TDCL";
                    break;
                case @"NL Third Division 17/18 South West":
                    subDivisionCode = @"TDSW";
                    break;
                case @"NL Third Division 17/18 South East":
                    subDivisionCode = @"TDSE";
                    break;
                /*case @"":
                    subDivisionCode = @"";
                    break;
                case @"":
                    subDivisionCode = @"";
                    break;
                case @"":
                    subDivisionCode = @"";
                    break;*/
                default:
                    throw new InvalidEnumArgumentException();
            }

            return subDivisionCode;
        }

        /// <summary>
        /// Get sub-division code in format: NL First Division North 17/18
        /// </summary>
        /// <param name="subDivision"></param>
        /// <returns></returns>
        private static string GetSubDivisionCodeFixtures(string subDivision)
        {
            string subDivisionCode;

            switch (subDivision)
            {
                //  FirstDivision
                case @"NL First Division North 17/18 Fixtures":
                    subDivisionCode = @"FDNH";
                    break;
                case @"NL First Division South 17/18 Fixtures":
                    subDivisionCode = @"FDSH";
                    break;
                //  Second Division
                case @"NL Second Division North 17/18 Fixtures":
                    subDivisionCode = @"SDNH";
                    break;
                case @"NL Second Division South 17/18 Fixtures":
                    subDivisionCode = @"SDSH";
                    break;
                //  Third Division
                case @"NL Third Division North 17/18 Fixtures":
                    subDivisionCode = @"TDNH";
                    break;
                case @"NL Third Division Central 17/18 Fixtures":
                    subDivisionCode = @"TDCL";
                    break;
                case @"NL Third Division 17/18 South West Fixtures":
                    subDivisionCode = @"TDSW";
                    break;
                case @"NL Third Division 17/18 South East Fixtures":
                    subDivisionCode = @"TDSE";
                    break;
                /*case @"":
                    subDivisionCode = @"";
                    break;
                case @"":
                    subDivisionCode = @"";
                    break;
                case @"":
                    subDivisionCode = @"";
                    break;*/
                default:
                    throw new InvalidEnumArgumentException();
            }

            return subDivisionCode;
        }

        /// <summary>
        /// Get club code for club
        /// </summary>
        /// <param name="clubName"></param>
        /// <returns></returns>
        private static string GetClubCode(string clubName)
        {
            string clubCode;

            switch (clubName)
            {
                //  FirstDivision
                case @"Sheffield Steelers 2":
                    clubCode = @"STL2";
                    break;
                case @"Coyotes":
                    clubCode = @"CYTE";
                    break;
                case @"St Mirren Warriors 1":
                    clubCode = @"STW1";
                    break;
                case @"Manchester Mavericks 1":
                    clubCode = @"MVR1";
                    break;
                case @"CWSC Panthers 1":
                    clubCode = @"CPT1";
                    break;
                case @"Knights 1":
                    clubCode = @"KNT1";
                    break;
                case @"Lancaster Bulldogs":
                    clubCode = @"LNBG";
                    break;
                case @"GLL & Aspire London Titans 2":
                    clubCode = @"ALT2";
                    break;
                case @"Tornados 1":
                    clubCode = @"TRD1";
                    break;
                case @"CWBA 2":
                    clubCode = @"CBA2";
                    break;
                case @"Sheffield Steelers 3":
                    clubCode = @"STL3";
                    break;
                case @"Exeter Otters":
                    clubCode = @"EXOS";
                    break;
                case @"The Bears 1":
                    clubCode = @"TBR1";
                    break;
                case @"Leicester Cobras 1":
                    clubCode = @"LSC1";
                    break;
                case @"Swindon Shock 1":
                    clubCode = @"SSK1";
                    break;
                //  Second Division
                case @"CWSC Panthers 2":
                    clubCode = @"CPT2";
                    break;
                case @"The Owls 2":
                    clubCode = @"OWL2";
                    break;
                case @"North Wales Knights 1":
                    clubCode = @"NWK1";
                    break;
                case @"Jaguars 1":
                    clubCode = @"JAG1";
                    break;
                case @"Newcastle Eagles 1":
                    clubCode = @"NWE1";
                    break;
                case @"Lothian Phoenix 1":
                    clubCode = @"LOP1";
                    break;
                case @"Wakefield Whirlwinds":
                    clubCode = @"WDWS";
                    break;
                case @"RGK Tees Valley Titans 2":
                    clubCode = @"TVT2";
                    break;
                case @"Essex Outlaws 2":
                    clubCode = @"ESO2";
                    break;
                case @"BlackHawks":
                    clubCode = @"BLHK";
                    break;
                case @"GLL & Aspire London Titans 3":
                    clubCode = @"ALT3";
                    break;
                case @"Cardiff Met Archers 1":
                    clubCode = @"CDA1";
                    break;
                case @"The Bears 2":
                    clubCode = @"TBR2";
                    break;
                case @"Tornados 2":
                    clubCode = @"TRD2";
                    break;
                case @"Sparrows 1":
                    clubCode = @"SPW1";
                    break;
                case @"Norwich Lowriders":
                    clubCode = @"NRWL";
                    break;
                //  Third Division
                case @"York Sharks":
                    clubCode = @"YKSK";
                    break;
                case @"Bolton Bulls 1":
                    clubCode = @"BLS1";
                    break;
                case @"Leeds Spiders 2":
                    clubCode = @"LSP2";
                    break;
                case @"RGK Tees Valley Titans 3":
                    clubCode = @"TVT3";
                    break;
                case @"CWSC Panthers 3":
                    clubCode = @"CPT3";
                    break;
                case @"North Wales Knights 2":
                    clubCode = @"NWK2";
                    break;
                case @"Calderdale WBC":
                    clubCode = @"CLDE";
                    break;
                case @"CWBA 3":
                    clubCode = @"CBA3";
                    break;
                case @"Sheffield Steelers 4":
                    clubCode = @"STL4";
                    break;
                case @"Jaguars 2":
                    clubCode = @"JAG2";
                    break;
                case @"Derby Wheelblazers 1":
                    clubCode = @"DRW1";
                    break;
                case @"The Bears 3":
                    clubCode = @"TBR3";
                    break;
                case @"Wolverhampton 1":
                    clubCode = @"WLV1";
                    break;
                case @"Leicester Cobras 2":
                    clubCode = @"LSC2";
                    break;
                case @"Swansea Storm 1":
                    clubCode = @"SWS1";
                    break;
                case @"Gloucester Blazers 1":
                    clubCode = @"GUB1";
                    break;
                case @"Hampshire Harriers 2":
                    clubCode = @"HMH2";
                    break;
                case @"Thames Valley Kings 1":
                    clubCode = @"TVK1";
                    break;
                case @"West Coast Warriors 1":
                    clubCode = @"WCW1";
                    break;
                case @"Worcester Wolves 1":
                    clubCode = @"WOR1";
                    break;
                case @"Plymouth Raiders":
                    clubCode = @"PLRA";
                    break;
                case @"Aces 1":
                    clubCode = @"ACE1";
                    break;
                case @"Sussex Bears 1":
                    clubCode = @"SUX1";
                    break;
                case @"Brixton Ballers 1":
                    clubCode = @"BRX1";
                    break;
                case @"Sparrows 2":
                    clubCode = @"SPW2";
                    break;
                case @"GLL & Aspire London Titans 4":
                    clubCode = @"ALT4";
                    break;
                case @"SportsAble Rockets":
                    clubCode = @"RCKS";
                    break;
                case @"Bury Bombers 1":
                    clubCode = @"BUR1";
                    break;
                case @"Hampshire Harriers 1":
                    clubCode = @"HMH1";
                    break;
                /*case @"":
                    clubCode = @"";
                    break;
                case @"":
                    clubCode = @"";
                    break;*/
                default:
                    throw new InvalidEnumArgumentException();
            }

            return clubCode;
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

        public static IEnumerable<GameResultParseDto> ParseResultDataSource(DataSourceResult source, string divisionCode, string nodeClassname)
        {
            var results = new List<GameResultParseDto>();
            var web = new HtmlWeb();
            var htmlDoc = web.Load(source.Url);
            var encrypter = new TyfSimpleAes();

            IEnumerable<string> htmlTables = ParseHtmlSplitTables(htmlDoc.ParsedText, nodeClassname);


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
                var subDivisionCode = GetSubDivisionCodeResults(subDivisionDetails);

                foreach (var tr in trsFiltered)
                {
                    // Get All the TD's
                    var tds = tr.Descendants("TD").ToArray();


                    var rslt = new GameResultParseDto
                    {
                        TimeStamp = GetDate(tds[0].InnerText.Trim()),
                        //  	Norwich Lowriders
                        HomeTeamName = tds[1].InnerText.Trim(),
                        Score = tds[2].InnerText.Trim(),
                        //  The Bears 2
                        AwayTeamName = tds[3].InnerText.Trim(),
                        Division = source.Division,
                        //  NL First Division North 17/18 Results
                        SubDivisionName = subDivisionDetails,
                        DivisionCode = divisionCode,
                        //  "Norwich Lowriders"
                        //HomeTeamCode = GetClubCode(tds[1].InnerText.Trim()),
                        //  "The Bears 2"
                        //AwayTeamCode = GetClubCode(tds[3].InnerText.Trim())
                    };

                    GetWinner(rslt);

                    //  Set HashedResult HomeTeam+AwayTeam+Score+TimeStamp
                    var hashedResult = rslt.HomeTeamName + rslt.AwayTeamName + rslt.Score + rslt.TimeStamp;
                    rslt.HashedResult = encrypter.Encrypt(hashedResult);
                    results.Add(rslt);
                }
            }

            return results;
        }

        public static IEnumerable<Rank> ParseRankingSource(DataSourceRanking source, string division,
            string divisionCode, string nodeClassname)
        {
            var results = new List<Rank>();
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
                var subDivisionCode = GetSubDivisionCode(subDivisionDetails);

                foreach (var tr in trsFiltered)
                {
                    // Get All the TD's
                    var tds = tr.Descendants("TD").ToArray();


                    var rslt = new Rank
                    {
                        Position = int.Parse((tds[0].InnerText.Trim())),
                        Team = tds[1].InnerText.Trim(),
                        GamesPlayed = tds[2].InnerText.Trim(),
                        GamesWon = int.Parse(tds[3].InnerText.Trim()),
                        GamesLost = tds[5].InnerText.Trim(),
                        Points = int.Parse(tds[10].InnerText.Trim()),
                        ClubCode = GetClubCode(tds[1].InnerText.Trim()),
                        SubDivision = subDivisionDetails,
                        SubDivisionCode = GetSubDivisionCode(subDivisionDetails),
                        Division = division,
                        DivisionCode = divisionCode,
                    };

                    results.Add(rslt);
                }
            }

            return results;
        }

        public static IEnumerable<FixtureResultParseDto> ParseFixturesDataSource(DataSourceFixture source, string division,
            string divisionCode, string nodeClassname)
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
                var subDivisionCode = GetSubDivisionCodeFixtures(subDivisionDetails);

                foreach (var tr in trsFiltered)
                {
                    // Get All the TD's
                    var tds = tr.Descendants("TD").ToArray();


                    var rslt = new FixtureResultParseDto
                    {
                        FixtureDate = GetDate(tds[0].InnerText.Trim()),
                        FixtureHomeTeamName = tds[1].InnerText.Trim(),
                        FixtureAwayTeamName = tds[2].InnerText.Trim(),
                        DivisionName = division,
                        DivisionCode = divisionCode,
                        SubDivisionName = subDivisionDetails,
                        SubDivisionCode = GetSubDivisionCodeFixtures(subDivisionDetails)
                    };

                    results.Add(rslt);
                }
            }

            return results;
        }
    }
}

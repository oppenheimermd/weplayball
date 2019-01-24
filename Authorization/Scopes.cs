
namespace WePlayBall.Authorization
{
    public static class WpbClaims
    {
        //  Claim name 25 characters max
        // the do:admin:thing scope is custom and defined in the scopes section of our API in the Auth0 dashboard
        
        /// <summary>
        /// Can read / edit specific team
        /// </summary>
        public const string ReadEditTeam = "readEditTeamData";

        /// <summary>
        /// Can read / edit all team data
        /// </summary>
        public const string ReadEditTeamsAll = "readEditTeamDataAll";

        /// <summary>
        /// Can run / read all reports
        /// </summary>
        public const string RunReadReportsAll = "runReadReportsAll";

        /// <summary>
        /// Can run / read reports for a specific team
        /// </summary>
        public const string RunReadReportsTeam = "runReadReportsTeam";
    }

    public static class WpbPolicy
    {
        /// <summary>
        /// Can read / edit a specific team
        /// </summary>
        public const string PolicyReadEditTeam = "readEdit:Team";
        /// <summary>
        /// Can read / edit all team data
        /// </summary>
        public const string PolicyReadEditTeamsAll = "readEdit:TeamsAll";

        /// <summary>
        /// Can run / read all reports
        /// </summary>
        public const string PolicyRunReadReportsAll = "runRead:ReportsAll";

        /// <summary>
        /// Can run / read reports for a specific team
        /// </summary>
        public const string PolicyRunReadReportTeam = "runRead:ReportsTeam";
    }
}

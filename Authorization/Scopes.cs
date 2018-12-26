
namespace WePlayBall.Authorization
{
    public static class WpbClaims
    {
        //  Claim name 25 characters max

        // the do:admin:thing scope is custom and defined in the scopes section of our API in the Auth0 dashboard
        /// <summary>
        /// All registered user are in this scope
        /// </summary>
        public const string ReadTeamData = "readTeamData";
    }

    public static class WpbPolicy
    {
        public const string PolicyReadTeamData = "read:TeamData";
    }
}

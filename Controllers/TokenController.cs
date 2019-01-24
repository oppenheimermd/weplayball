using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WePlayBall.Authorization;
using WePlayBall.Helpers.Api.ApiErrors;
using WePlayBall.Models;
using WePlayBall.Models.DTO;
using WePlayBall.Service;
using WePlayBall.Settings;

namespace WePlayBall.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : Controller
    {
        private IConfiguration _config;
        private readonly IWPBService _wpbService;
        private readonly SiteConfig _siteSettings;

        public TokenController(IConfiguration config, IWPBService wpbService, SiteConfig siteSettings)
        {
            _config = config;
            _wpbService = wpbService;
            _siteSettings = siteSettings;
        }



        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> CreateToken([FromBody]LoginModelDto login)
        {
            var user = await _wpbService.AuthenticateAsync(login.Username, login.Password);
            if (user == null)
                return BadRequest(new ApiError(400, "BadRequest", "Username or password was incorrect."));

            var tokenString = GenerateToken(user);

            return Ok(
                new
                {
                    Id = user.Id.ToString(),
                    user.Username,
                    user.FirstName,
                    user.Email,
                    Token = tokenString
                });
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]RegisterModelDto registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiError(400, "BadRequest", "Password and username required."));
            }

            var newUser = new User()
            {
                FirstName = registerModel.FirstName.ToLowerInvariant(),
                Email = registerModel.Email.ToLowerInvariant(),
                Username = registerModel.Username.ToLowerInvariant(),
            };


            //  Username unique?
            var isUsernameUnique = await _wpbService.UsernameUnique(newUser.Username);
            if (!isUsernameUnique)
                return BadRequest(new ApiError(400, "BadRequest", $"Username: {registerModel.Username} is already in use."));

            //  Email unique?
            var isEmailUnique = await _wpbService.EmailUnique(newUser.Email);
            if (!isEmailUnique)
                return BadRequest(new ApiError(400, "BadRequest", $"Email: {registerModel.Email} is already in use."));

            //  Add user
            await _wpbService.CreateUserAsync(newUser, registerModel.Password);

            //  Build new user token
            var tokenString = GenerateToken(newUser);


            // This is just a regular member so we don't need to create any claims
            //  Add user claim
            /*var newClaim = new UserClaim()
            {
                UserId = newUser.Id,
                ClaimName = WpbClaims.ReadTeamData
            };*/

            return Ok(
                new
                {
                    Id = newUser.Id.ToString(),
                    newUser.Username,
                    newUser.FirstName,
                    newUser.Email,
                    Token = tokenString
                });

        }

        //  Old
        /*private async Task<string> BuildTokenAsync(User user)
        {
            /*var claims = new[] {
                //  Web Claim:  preferred_username
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                //  Web Claim:  email
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                //  Web Claim:  given_name  (we need only first name)
                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                //  Web Claim:  birthdate
                new Claim(JwtRegisteredClaimNames.Birthdate, user.Birthdate.ToString("yyyy-MM-dd")),
                //  Web Claim:  jti
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                //  Claim TeamMember can read all public data
                new Claim(WpbClaims.ReadTeamData,"")
            };*/

            /*var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Id.ToString())
            };

            var userClaims = await _wpbService.GetUserClaimsAsync(user.Id);
            claims.AddRange(userClaims.Select(claim => new Claim(claim.ClaimName, true.ToString())));


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims.ToArray(),
                expires: DateTime.UtcNow.AddMonths(12),// set expiry a year from now
                signingCredentials: creds);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }*/

        private string GenerateToken(User user)
        {
            var claims = new Claim[] {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddMonths(12)).ToUnixTimeSeconds().ToString()),
            };
            
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims.ToArray(),
                expires: DateTime.UtcNow.AddMonths(12),// set expiry a year from now
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}

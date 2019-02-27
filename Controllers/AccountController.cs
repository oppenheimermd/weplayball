using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WePlayBall.Authorization;
using WePlayBall.Models;
using WePlayBall.Service;
using WePlayBall.Settings;
using WePlayBall.Models.DTO;

namespace WePlayBall.Controllers
{
    //  see: http://oloshcoder.com/2018/05/21/jwt-token-with-cookie-authentication-in-asp-net-core/

    public class AccountController : Controller
    {
        private readonly IWPBService _wpbService;
        private readonly IConfiguration _config;

        public AccountController(IWPBService wpbService, IConfiguration config)
        {
            _wpbService = wpbService;
            _config = config;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            // If the user is already authenticated we do not need to display the login page, so we redirect to the landing page. 
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(FrontEndLogin model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                var user = await _wpbService.AuthenticateAsync(model.Username, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Username or password was incorrect");
                    return View(model);
                }
                else
                {
                    var claims = await BuildClaimsAsync(user);
                    var userIdentity = new ClaimsIdentity(claims, "login");
                    var principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        principal,
                        //  https://tahirnaushad.com/2017/09/08/asp-net-core-2-0-cookie-authentication/
                        new AuthenticationProperties
                        {
                            ExpiresUtc = DateTime.UtcNow.AddDays(30)
                        });
                    return returnUrl != null ? RedirectToLocal(returnUrl) : RedirectToAction("Index", "Home");
                }
                   
            }
            
        }

        //[HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Account/Login");
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task<List<Claim>> BuildClaimsAsync(User user)
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Email)
            };

            var userClaims = await _wpbService.GetUserClaimsAsync(user.Id);
            claims.AddRange(userClaims.Select(claim => new Claim(claim.ClaimName, true.ToString())));

            return claims;
        }

        /// <summary>
        ///  Run once only!!
        /// </summary>
        /// <returns></returns>
        private async Task BuildAdmin()
        {
            var user = new User()
            {
                Email = "",
                FirstName = "",
                Username = ""
            };
            var password = "";


            //  Add user
            await _wpbService.CreateUserAsync(user, password);

            var claimsList = new List<UserClaim>()
            {
                new UserClaim()
                {
                    UserId = user.Id,
                    ClaimName = WpbClaims.ReadEditTeam
                },
                new UserClaim()
                {
                    UserId = user.Id,
                    ClaimName = WpbClaims.ReadEditTeamsAll
                },
                new UserClaim()
                {
                    UserId = user.Id,
                    ClaimName = WpbClaims.RunReadReportsAll
                },
                new UserClaim()
                {
                    UserId = user.Id,
                    ClaimName = WpbClaims.RunReadReportsTeam
                }
            };

            foreach (var claim in claimsList)
            {
                await _wpbService.AddUserClaimAsync(claim);
            }
        }
    }
}
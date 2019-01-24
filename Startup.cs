using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WePlayBall.Authorization;
using WePlayBall.Data;
using WePlayBall.Service;
using WePlayBall.Settings;

namespace WePlayBall
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //  Register JWT authentication schema by using AddAuthentication method and specifying
            //  JwtBearerDefaults.AuthenticationScheme
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        //  validate the server that created that token
                        ValidateIssuer = true,
                        //  ensure that the recipient of the token is authorized to receive it 
                        ValidateAudience = true,
                        //  check that the token is not expired and that the signing key of the issuer is 
                        ValidateLifetime = true,
                        //  verify that the key used to sign the incoming token is part of a list of trusted keys 
                        ValidateIssuerSigningKey = true,
                        //  specify the values for the issuer, the audience and the signing key. These values
                        //  are sotred in the appsettings.json file to made accessible via Configuration object:
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(WpbPolicy.PolicyReadEditTeam, policy => policy.RequireClaim(WpbClaims.ReadEditTeam));
                options.AddPolicy(WpbPolicy.PolicyReadEditTeamsAll, policy => policy.RequireClaim(WpbClaims.ReadEditTeamsAll));
                options.AddPolicy(WpbPolicy.PolicyRunReadReportsAll, policy => policy.RequireClaim(WpbClaims.RunReadReportsAll));
                options.AddPolicy(WpbPolicy.PolicyRunReadReportTeam, policy => policy.RequireClaim(WpbClaims.RunReadReportsTeam));
            });


            //  include support for CORS
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .Build());
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //  EDIT Monday 21, January 2019
            //  see:    http://oloshcoder.com/2018/05/21/jwt-token-with-cookie-authentication-in-asp-net-core/
            services.AddAuthentication(o => {
                o.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options => {
                options.AccessDeniedPath = new PathString("/Account/Login/");
                options.LoginPath = new PathString("/Account/Login/");
            });

            services.AddScoped<IWPBService, WPBService>();

            //  This provides you a filled config object instance that has values set from the various
            //  configuration stores. 
            var config = new SiteConfig();
            Configuration.Bind("SiteSettings", config);
            services.AddSingleton(config);


            //  Register blog context with dependency injection
            services.AddDbContext<WPBDataContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:WPBContextString"], 
               opt => opt.EnableRetryOnFailure()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //  EDIT Monday 21, January 2019
            //  see:    http://oloshcoder.com/2018/05/21/jwt-token-with-cookie-authentication-in-asp-net-core/
            app.UseAuthentication();

            app.UseStaticFiles();

            //  In order to make the authentication service available to the application, we need to
            //  to invoke app.UseAuthentication():
            app.UseAuthentication();

            //  Allow Cors
            app.UseCors("CorsPolicy");


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


    }
}

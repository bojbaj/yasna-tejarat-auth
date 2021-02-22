using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using YT.Challenge.Auth.DB;
using YT.Challenge.Auth.i18n;
using YT.Challenge.Auth.Models;
using YT.Challenge.Auth.Services;

namespace YT.Challenge.Auth
{
    public static class Init
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<AuthDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDbContext"));
            });

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })

            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]))
                };
            });

            services.AddScoped<IUserService, UserService>();
            services.AddSingleton<IMessageRepo, MessageRepo>();
        }

        public static void CreateAndMigrateAuthDB(this AuthDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            dbContext.Database.Migrate();
            SeedUsers(userManager, dbContext);
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager, AuthDbContext dbContext)
        {
            ApplicationUser defaultUser = new ApplicationUser
            {
                UserName = "bojbaj",
                Email = "bojbaj@gmail.com"
            };
            if (userManager.FindByEmailAsync(defaultUser.Email).Result == null)
            {
                string defaultUserPassword = "Pwd123456@";
                IdentityResult result = userManager.CreateAsync(defaultUser, defaultUserPassword).Result;
            }
        }
    }
}
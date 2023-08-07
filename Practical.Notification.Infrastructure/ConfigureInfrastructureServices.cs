using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Practical.Notification.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Practical.Notification.Infrastructure.Persistence;

namespace Practical.Notification.Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddDbContext<PortalContext>(options =>
      {
          options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
      });
                
      services.AddScoped<PortalContextInitialiser>();
     
      services
            .AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<PortalContext>()
            .AddDefaultTokenProviders();
      
    services.AddScoped<UserManager<ApplicationUser>>();
    services.AddScoped<SignInManager<ApplicationUser>>();

    var jwtSettings = configuration.GetSection("JwtSettings");
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
    services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["Issuer"],
                ValidAudience = jwtSettings["Audience"],
                IssuerSigningKey = key
            };
        });
        return services;
    }
}
using JobSity.Chatroom.Application.Shared.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Identity.Data;
using NetDevPack.Identity.Jwt;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace JobSity.Chatroom.Application.Shared.Identity
{
    [ExcludeFromCodeCoverage]
    public static class ApiIdentityConfig
    {
        public static IServiceCollection AddApiIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            // Default EF Context for Identity (inside of the NetDevPack.Identity)
            services.AddIdentityEntityFrameworkContextConfiguration(options =>
                         options.UseNpgsql(connectionStrings.Postgres.GetConnectionString(),
                    b => b.MigrationsAssembly("JobSity.Chatroom.Application")));

            // Identity configuration
            services
                .AddIdentity<IdentityUser, IdentityRole>(x =>
                {
                    x.Password.RequiredLength = 6;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireNonAlphanumeric = false;
                    x.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<NetDevPackAppDbContext>()
                .AddDefaultTokenProviders();

            // JWT configuration
            return services.AddJwtConfiguration(configuration, "AppSettings");
        }

        private static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration, string appJwtSettingsKey = "AppJwtSettings")
        {
            if (services == null) throw new ArgumentException(nameof(services));
            if (configuration == null) throw new ArgumentException(nameof(configuration));

            var appSettingsSection = configuration.GetSection(appJwtSettingsKey);

            services.AddOptions<AppJwtSettings>()
               .Configure<IConfiguration>((settings, configuration) => { configuration.GetSection(appJwtSettingsKey).Bind(settings); });

            var appSettings = appSettingsSection.Get<AppJwtSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = true;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = appSettings.Audience,
                        ValidIssuer = appSettings.Issuer
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Headers.Authorization.FirstOrDefault();
                            if (accessToken != null)
                            {
                                context.Token = accessToken?.Replace("Bearer ", string.Empty);
                            }
                            else if (context.HttpContext.Request.Path.StartsWithSegments("/chat-room-hub"))
                            {
                                accessToken = context.Request.Query["access_token"];
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }
    }
}
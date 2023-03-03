using JobSity.Chatroom.Application.Shared.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using NetDevPack.Identity.Data;
using NetDevPack.Identity.Jwt;
using System.Text;

namespace JobSity.Chatroom.Application.Shared.Identity
{
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
                    x.Password.RequiredLength = 1;
                    x.Password.RequireUppercase = false;
                    x.Password.RequireLowercase = false;
                    x.Password.RequireNonAlphanumeric = false;
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
                            context.Token = accessToken;

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/chat-room-hub"))
                            {
                                // Read the token out of the query string
                            }
                            return Task.CompletedTask;
                        }
                    };
                });

            return services;
        }

        private static void SymetricKeyConfiguration(IServiceCollection services, AppJwtSettings appSettings)
        {
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
                });
        }
    }
}
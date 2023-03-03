using JobSity.Chatroom.Application.Shared.AutoMapper;
using JobSity.Chatroom.Application.Shared.Chat.Repositories;
using JobSity.Chatroom.Application.Shared.Chat.Services;
using JobSity.Chatroom.Application.Shared.Configurations;
using JobSity.Chatroom.Application.Shared.Data.Postgre;
using JobSity.Chatroom.Application.Shared.Identity;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.Validator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Shared.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class SharedExtensions
    {
        public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
        {
            return services
               .AddConnectionStrings()
               .AddPostgres(configuration) // Setting DBContexts
               .AddValidatorService()
               .AddNotificationDependencyInjections()
               .AddAutoMapperConfiguration()
               .AddWebAppIdentityConfiguration(configuration)
               .AddApiIdentityConfiguration(configuration) // ASP.NET Identity Settings & JWT
               .AddSaleDependencyInjections();
        }

        private static IServiceCollection AddConnectionStrings(this IServiceCollection services)
        {
            services.AddOptions<ConnectionStrings>()
               .Configure<IConfiguration>((settings, configuration) => { configuration.GetSection("ConnectionStrings").Bind(settings); });

            return services;
        }
        
        private static IServiceCollection AddPostgres(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            services.AddDbContext<DataContext>(
                options => options.UseNpgsql(connectionStrings.Postgres.GetConnectionString())
            );

            return services;
        }

        private static IServiceCollection AddValidatorService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IValidatorService<>), typeof(ValidatorService<>));

            return services;
        }

        private static IServiceCollection AddNotificationDependencyInjections(this IServiceCollection services)
        {
            services.TryAddScoped<INotificationContext, NotificationContext>();

            return services;
        }
        
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(DomainToViewModelMappingProfile), typeof(ViewModelToDomainMappingProfile));

            return services;
        }

        public static IServiceCollection AddWebAppIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Default EF Context for Identity (inside of the NetDevPack.Identity)
            var connectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

            // Default EF Context for Identity (inside of the NetDevPack.Identity)
            services.AddIdentityEntityFrameworkContextConfiguration(options =>
                         options.UseNpgsql(connectionStrings.Postgres.GetConnectionString(),
                    b => b.MigrationsAssembly("JobSity.Chatroom.Application")));

            // Default Identity configuration from NetDevPack.Identity
            //services.AddIdentityConfiguration();
            
            return services;
        }
        

        //public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        //{
        //    if (services == null) throw new ArgumentNullException(nameof(services));

        //    services.AddDbContext<DataContext>(options =>
        //        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        //    services.AddDbContext<EventStoreSqlContext>(options =>
        //        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        //}

        private static IServiceCollection AddSaleDependencyInjections(this IServiceCollection services)
        {
            services.TryAddScoped<IChatMessageService, ChatMessageService>();
            services.TryAddScoped<IChatMessageRepository, ChatMessageRepository>();

            return services;
        }
    }
}
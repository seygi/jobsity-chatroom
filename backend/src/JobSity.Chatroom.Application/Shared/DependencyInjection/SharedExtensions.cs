using JobSity.Chatroom.Application.Shared.AutoMapper;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Services;
using JobSity.Chatroom.Application.Shared.Chatrooms.Services;
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
               .AddChatMessageDependencyInjections()
               .AddChatRoomDependencyInjections();
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

            return services;
        }

        private static IServiceCollection AddChatMessageDependencyInjections(this IServiceCollection services)
        {
            services.TryAddScoped<IChatMessageService, ChatMessageService>();
            services.TryAddScoped<IChatMessageRepository, ChatMessageRepository>();

            return services;
        }

        private static IServiceCollection AddChatRoomDependencyInjections(this IServiceCollection services)
        {
            services.TryAddScoped<IChatRoomService, ChatRoomService>();
            services.TryAddScoped<IChatRoomRepository, ChatRoomRepository>();

            return services;
        }
    }
}
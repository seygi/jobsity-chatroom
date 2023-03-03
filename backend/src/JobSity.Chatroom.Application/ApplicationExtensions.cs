using JobSity.Chatroom.Application.Features.Chatroom.CreateMessage.DependencyInjection;
using JobSity.Chatroom.Application.Shared.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationExtensions
    {
        public const string ApplicationName = "JobSity.Chatroom";

        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddShared(configuration)
               .AddCreateMessage();
            //   .AddGetSales()
            //   .AddGetSellers();

            return services;
        }
    }
}

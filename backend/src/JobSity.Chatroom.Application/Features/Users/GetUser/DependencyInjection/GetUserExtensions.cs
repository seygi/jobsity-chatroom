using JobSity.Chatroom.Application.Features.Users.GetUser.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Features.Users.GetUser.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class GetUserExtensions
    {
        public static IServiceCollection AddGetUser(this IServiceCollection services)
        {
            services.TryAddScoped<IUseCase<DefaultInput, GetUserOutput>, GetUserUseCase>();

            return services;
        }
    }
}

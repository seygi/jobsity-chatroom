using FluentValidation;
using JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase;
using JobSity.Chatroom.Application.Features.Users.LoginUser.Validators;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Features.Users.LoginUser.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class LoginUserExtensions
    {
        public static IServiceCollection AddLoginUser(this IServiceCollection services)
        {
            services.TryAddSingleton<IValidator<LoginUserInput>, LoginUserValidator>();
            services.TryAddScoped<IUseCase<LoginUserInput, LoginUserOutput>, LoginUserUseCase>();

            return services;
        }
    }
}

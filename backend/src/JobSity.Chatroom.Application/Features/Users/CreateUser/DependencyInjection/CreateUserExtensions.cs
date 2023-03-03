using FluentValidation;
using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.Validators;
using JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class CreateUserExtensions
    {
        public static IServiceCollection AddCreateUser(this IServiceCollection services)
        {
            services.TryAddSingleton<IValidator<CreateUserInput>, CreateUserValidator>();
            services.TryAddScoped<IUseCase<CreateUserInput, CreateUserOutput>, CreateUserUseCase>();

            return services;
        }
    }
}

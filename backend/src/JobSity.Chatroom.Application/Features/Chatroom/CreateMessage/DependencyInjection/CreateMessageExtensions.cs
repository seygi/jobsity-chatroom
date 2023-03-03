using FluentValidation;
using JobSity.Chatroom.Application.Features.Chatroom.ChatroomCreateMessage.UseCase;
using JobSity.Chatroom.Application.Features.Chatroom.CreateMessage.Validators;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Features.Chatroom.CreateMessage.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class CreateMessageExtensions
    {
        public static IServiceCollection AddCreateMessage(this IServiceCollection services)
        {
            services.TryAddSingleton<IValidator<CreateMessageInput>, CreateMessageValidator>();
            services.TryAddScoped<IUseCase<CreateMessageInput, DefaultOutput>, CreateMessageUseCase>();

            return services;
        }
    }
}

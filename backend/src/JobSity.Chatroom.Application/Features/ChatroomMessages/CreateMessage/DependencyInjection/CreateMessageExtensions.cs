using FluentValidation;
using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.Validators;
using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.UseCase;
using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.Validators;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class CreateMessageExtensions
    {
        public static IServiceCollection AddCreateMessage(this IServiceCollection services)
        {
            services.TryAddSingleton<IValidator<CreateMessageInput>, CreateMessageValidator>();
            services.TryAddScoped<IUseCase<CreateMessageInput, CreateMessageOutput>, CreateMessageUseCase>();

            return services;
        }
    }
}

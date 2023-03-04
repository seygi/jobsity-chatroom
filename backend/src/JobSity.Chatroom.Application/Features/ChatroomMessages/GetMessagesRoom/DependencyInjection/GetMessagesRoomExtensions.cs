using FluentValidation;
using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase;
using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.Validators;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class GetMessagesRoomExtensions
    {
        public static IServiceCollection AddGetMessagesRoom(this IServiceCollection services)
        {
            services.TryAddSingleton<IValidator<GetMessagesRoomInput>, GetMessagesRoomValidator>();
            services.TryAddScoped<IUseCase<GetMessagesRoomInput, GetMessagesRoomListOutput>, GetMessagesRoomUseCase>();

            return services;
        }
    }
}

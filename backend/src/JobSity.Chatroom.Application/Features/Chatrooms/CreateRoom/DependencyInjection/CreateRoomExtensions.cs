using FluentValidation;
using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.UseCase;
using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.Validators;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class CreateRoomExtensions
    {
        public static IServiceCollection AddCreateRoom(this IServiceCollection services)
        {
            services.TryAddSingleton<IValidator<CreateRoomInput>, CreateRoomValidator>();
            services.TryAddScoped<IUseCase<CreateRoomInput, CreateRoomOutput>, CreateRoomUseCase>();

            return services;
        }
    }
}

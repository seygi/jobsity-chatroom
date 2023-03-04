using JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class GetAllRoomsExtensions
    {
        public static IServiceCollection AddGetAllRooms(this IServiceCollection services)
        {
            services.TryAddScoped<IUseCase<DefaultInput, GetAllRoomsListOutput>, GetAllRoomsUseCase>();

            return services;
        }
    }
}

using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.DependencyInjection;
using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.DependencyInjection;
using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.DependencyInjection;
using JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.DependencyInjection;
using JobSity.Chatroom.Application.Features.Users.LoginUser.DependencyInjection;
using JobSity.Chatroom.Application.Shared.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services
               .AddShared(configuration)
               .AddCreateUser()
               .AddLoginUser()
               .AddCreateRoom()
               .AddGetAllRooms()
               .AddCreateMessage()
               .AddGetMessagesRoom();

            return services;
        }
    }
}

using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.DependencyInjection;
using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.DependencyInjection;
using JobSity.Chatroom.Application.Features.ChatroomMessages.SyncBotMessages.DependencyInjection;
using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.DependencyInjection;
using JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.DependencyInjection;
using JobSity.Chatroom.Application.Features.Stocks.SearchStock.DependencyInjection;
using JobSity.Chatroom.Application.Features.Users.GetUser.DependencyInjection;
using JobSity.Chatroom.Application.Features.Users.LoginUser.DependencyInjection;
using JobSity.Chatroom.Application.Shared.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("JobSity.Chatroom.Tests.Unit")]
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
               .AddGetUser()
               .AddCreateRoom()
               .AddGetAllRooms()
               .AddCreateMessage()
               .AddGetMessagesRoom()
               .AddSearchStock()
               .AddSyncBotMessages();

            return services;
        }
    }
}

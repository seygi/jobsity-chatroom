using JobSity.Chatroom.Application.Features.ChatroomMessages.SyncBotMessages.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.SyncBotMessages.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class SyncBotMessagesExtensions
    {
        public static IServiceCollection AddSyncBotMessages(this IServiceCollection services)
        {
            services.TryAddScoped<IUseCase<SyncBotMessagesInput, SyncBotMessagesOutput>, SyncBotMessagesUseCase>();

            return services;
        }
    }
}

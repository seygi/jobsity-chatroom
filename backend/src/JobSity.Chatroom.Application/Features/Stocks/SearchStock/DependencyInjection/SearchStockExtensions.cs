using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Features.Stocks.SearchStock.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Features.Stocks.SearchStock.DependencyInjection
{
    [ExcludeFromCodeCoverage]
    internal static class SearchStockExtensions
    {
        public static IServiceCollection AddSearchStock(this IServiceCollection services)
        {
            services.TryAddScoped<IUseCase<SearchStockInput, SearchStockOutput>, SearchStockUseCase>();

            return services;
        }
    }
}

using JobSity.Chatroom.Application.Shared.Stocks.Entities;
using JobSity.Chatroom.Application.Shared.Stocks.Model;

namespace JobSity.Chatroom.Application.Shared.Stocks.Services
{
    public interface IStockService
    {
        Task EnqueueStockToSearchAsync(Stock stock, CancellationToken cancellationToken);
        Task SubscribeAsync(Func<Stock, Task> handler);
        Task<StockResponse> GetStockAsync(string stockCode, CancellationToken cancellationToken);
    }
}
using JobSity.Chatroom.Application.Shared.Messaging;
using JobSity.Chatroom.Application.Shared.Stocks.Entities;
using JobSity.Chatroom.Application.Shared.Stocks.Model;

namespace JobSity.Chatroom.Application.Shared.Stocks.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _client;
        private readonly IBus _bus;
        private readonly string _stockQueueName = "stocksQueue";
        public StockService(IBus bus, HttpClient client)
        {
            _bus = bus;
            _client = client;
        }

        public Task EnqueueStockToSearchAsync(Stock stock, CancellationToken cancellationToken)
        {
            return _bus.PublishAsync(stock, _stockQueueName);
        }

        public async Task SubscribeAsync(Func<Stock, Task> handler)
        {
            await _bus.SubscribeAsync(handler, _stockQueueName);
        }
        public async Task<StockResponse> GetStockAsync(string stockCode, CancellationToken cancellationToken)
        {
            var uri = $"?s={stockCode}&f=sd2t2ohlcv&h&e=csv";
            var response = await _client.GetAsync(uri);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                return StockResponse.Empty;

            string body = await response.Content.ReadAsStringAsync();
            var data = body.Substring(body.IndexOf(Environment.NewLine, StringComparison.Ordinal) + 2);
            var processedArray = data.Split(',');
            var stock = new StockResponse()
            {
                Symbol = processedArray[0],
                Date = !processedArray[1].Contains("N/D") ? Convert.ToDateTime(processedArray[1]) : default,
                Time = !processedArray[2].Contains("N/D") ? Convert.ToDateTime(processedArray[2]).TimeOfDay : default,
                Open = !processedArray[3].Contains("N/D") ? Convert.ToDecimal(processedArray[3]) / 100 : default,
                High = !processedArray[4].Contains("N/D") ? Convert.ToDecimal(processedArray[4]) / 100 : default,
                Low = !processedArray[5].Contains("N/D") ? Convert.ToDecimal(processedArray[5]) / 100 : default,
                Close = !processedArray[6].Contains("N/D") ? Convert.ToDecimal(processedArray[6]) / 100 : default,
                Volume = !processedArray[7].Contains("N/D") ? Convert.ToDecimal(processedArray[7]) / 100 : default,
            };
            
            return stock;
        }
    }
}
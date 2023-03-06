using Flurl.Http;
using JobSity.Chatroom.Application.Shared.Configurations.Bot;
using JobSity.Chatroom.Application.Shared.Identity;
using JobSity.Chatroom.Application.Shared.Messaging;
using JobSity.Chatroom.Application.Shared.Stocks.Entities;
using JobSity.Chatroom.Application.Shared.Stocks.Model;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace JobSity.Chatroom.Application.Shared.Stocks.Services
{
    public class StockService : IStockService
    {
        private readonly HttpClient _client;
        private readonly IBus _bus;
        private readonly string _stockQueueName = "stocksQueue";
        private readonly BotConfiguration _botConfiguration;
        private readonly ITokenService _tokenService;
        public StockService(IBus bus, HttpClient client, ITokenService tokenService, IOptionsSnapshot<BotConfiguration> configuration)
        {
            _bus = bus;
            _client = client;
            _tokenService = tokenService;
            _botConfiguration = configuration.Value;
        }

        public Task EnqueueStockToSearchAsync(Stock stock, CancellationToken cancellationToken)
        {
            var _botToken = _tokenService.GenerateBotToken();

            var url = $"{_botConfiguration.BaseUrl}/api/BotStock";

            return url
                .WithHeader("Authorization", $"Bearer {_botToken}")
                .PostJsonAsync(stock);
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

            if (data.Contains("N/D"))
                return StockResponse.Empty;

            var processedArray = data.Split(',');
            string symbol = processedArray[0];
            DateTime date = !processedArray[1].Contains("N/D") ? Convert.ToDateTime(processedArray[1]) : default;
            TimeSpan time = !processedArray[2].Contains("N/D") ? Convert.ToDateTime(processedArray[2]).TimeOfDay : default;
            decimal open = !processedArray[3].Contains("N/D") ? Convert.ToDecimal(processedArray[3], CultureInfo.InvariantCulture) : default;
            decimal high = !processedArray[4].Contains("N/D") ? Convert.ToDecimal(processedArray[4], CultureInfo.InvariantCulture) : default;
            decimal low = !processedArray[5].Contains("N/D") ? Convert.ToDecimal(processedArray[5], CultureInfo.InvariantCulture) : default;
            decimal close = !processedArray[6].Contains("N/D") ? Convert.ToDecimal(processedArray[6], CultureInfo.InvariantCulture) : default;
            int volume = !processedArray[7].Contains("N/D") ? Convert.ToInt32(processedArray[7]) : default;

            return new StockResponse(symbol, date, time, open, high, low, close, volume);
        }
    }
}
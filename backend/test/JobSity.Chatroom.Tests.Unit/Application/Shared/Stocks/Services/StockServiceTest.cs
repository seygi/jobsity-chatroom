using JobSity.Chatroom.Application.Shared.Messaging;
using JobSity.Chatroom.Application.Shared.Stocks.Entities;
using JobSity.Chatroom.Application.Shared.Stocks.Model;
using JobSity.Chatroom.Application.Shared.Stocks.Services;
using JobSity.Chatroom.Tests.Unit;
using NSubstitute;
using System.Net;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Stocks.Services
{
    public class StockServiceTest
    {
        private HttpClient _client;
        private IBus _bus;
        private IStockService _stockService;

        public StockServiceTest()
        {
            _bus = Substitute.For<IBus>();
            var response = @"";
            var messageHandler = new MockHttpMessageHandler(response, HttpStatusCode.InternalServerError);
            _client = new HttpClient(messageHandler)
            {
                BaseAddress = new Uri("http://not-important.com")
            };
            _stockService = new StockService(_bus, _client);
        }

        [Fact(DisplayName = "ShouldReturnEmptyWhenStockStatusCodeIsNotSuccess")]
        public async Task ShouldReturnEmptyWhenStockStatusCodeIsNotSuccess()
        {
            // arrange
            var response = @"";
            var messageHandler = new MockHttpMessageHandler(response, HttpStatusCode.InternalServerError);
            _client = new HttpClient(messageHandler)
            {
                BaseAddress = new Uri("http://not-important.com")
            };
            _stockService = new StockService(_bus, _client);

            // act
            var result = await _stockService.GetStockAsync("None", CancellationToken.None);

            // assert
            _client.ReceivedCalls().Should().HaveCount(1);
            _bus.DidNotReceive();
            result.Should().BeEquivalentTo(StockResponse.Empty);
        }

        [Fact(DisplayName = "ShouldReturnStockWithDefaultValuesWhenTickerNotExists")]
        public async Task ShouldReturnStockWithDefaultValuesWhenTickerNotExists()
        {
            // arrange
            var response = @"Symbol,Date,Time,Open,High,Low,Close,Volume
TSLA,N/D,N/D,N/D,N/D,N/D,N/D,N/D";
            var messageHandler = new MockHttpMessageHandler(response, HttpStatusCode.OK);
            _client = new HttpClient(messageHandler)
            {
                BaseAddress = new Uri("http://not-important.com")
            };
            _stockService = new StockService(_bus, _client);

            var symbol = "TSLA";

            // act
            var result = await _stockService.GetStockAsync(symbol, CancellationToken.None);
            var defaultStock = new StockResponse(symbol, default, default, default, default, default, default, default);

            // assert
            _client.ReceivedCalls().Should().HaveCount(1);
            _bus.DidNotReceive();
            result.Should().BeEquivalentTo(defaultStock);
        }

        [Fact(DisplayName = "ShouldReturnStockWhenTickerExists")]
        public async Task ShouldReturnStockWhenTickerExists()
        {
            // arrange
            var response = @"Symbol,Date,Time,Open,High,Low,Close,Volume
AAPL.US,2023-03-02,22:00:03,144.38,146.71,143.9,145.91,52576265";
            var messageHandler = new MockHttpMessageHandler(response, HttpStatusCode.OK);
            _client = new HttpClient(messageHandler)
            {
                BaseAddress = new Uri("http://not-important.com")
            };
            _stockService = new StockService(_bus, _client);

            var symbol = "AAPL.US";

            // act
            var result = await _stockService.GetStockAsync(symbol, CancellationToken.None);
            var defaultStock = new StockResponse(symbol, DateTime.Parse("2023-03-02"), TimeSpan.Parse("22:00:03"), 144.38M, 146.71M, 143.9M, 145.91M, 52576265);

            // assert
            _client.ReceivedCalls().Should().HaveCount(1);
            _bus.DidNotReceive();
            result.Should().BeEquivalentTo(defaultStock);
        }

        [Fact(DisplayName = "ShouldReturnCompletedTaskWhenCallSubscribeAsync")]
        public void ShouldReturnCompletedTaskWhenCallSubscribeAsync()
        {
            // arrange
            var task = Task.CompletedTask;
            _bus
                .SubscribeAsync(Arg.Any<Func<Stock, Task>>(), Arg.Any<string>())
                .Returns(task);

            // act
            var result = _stockService.SubscribeAsync(Arg.Any<Func<Stock, Task>>());

            // assert
            result.Should().BeEquivalentTo(task);
        }

        [Fact(DisplayName = "ShouldReturnCompletedTaskWhenCallEnqueueStockToSearchAsync")]
        public void ShouldReturnCompletedTaskWhenCallEnqueueStockToSearchAsync()
        {
            // arrange
            _bus
                .PublishAsync(Arg.Any<Stock>(), Arg.Any<string>())
                .Returns(Task.CompletedTask);

            // act
            var result = _stockService.EnqueueStockToSearchAsync(Stock.Create("", Guid.Empty), CancellationToken.None);

            // assert
            result.Should().BeEquivalentTo(Task.CompletedTask);
        }
    }
}
using Flurl.Http;
using Flurl.Http.Testing;
using JobSity.Chatroom.Application.Shared.Configurations.Bot;
using JobSity.Chatroom.Application.Shared.Identity;
using JobSity.Chatroom.Application.Shared.Messaging;
using JobSity.Chatroom.Application.Shared.Stocks.Entities;
using JobSity.Chatroom.Application.Shared.Stocks.Model;
using JobSity.Chatroom.Application.Shared.Stocks.Services;
using JobSity.Chatroom.Tests.Unit;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;
using Newtonsoft.Json;
using NSubstitute;
using System.Net;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Stocks.Services
{
    public class StockServiceTest
    {
        private HttpClient _client;
        private IBus _bus;
        private IStockService _stockService;
        private ITokenService _tokenService;
        private readonly IOptionsSnapshot<BotConfiguration> _botConfiguration;
        private readonly HttpTest _httpTest;

        public StockServiceTest()
        {
            _httpTest = new HttpTest();
            _bus = Substitute.For<IBus>();
            _botConfiguration = Substitute.For<IOptionsSnapshot<BotConfiguration>>();
            _botConfiguration.Value.Returns(
                new BotConfiguration
                {
                    BaseUrl = "http://localhost:1211"
                }
            );

            _tokenService = Substitute.For<ITokenService>();
            var response = @"";
            var messageHandler = new MockHttpMessageHandler(response, HttpStatusCode.InternalServerError);
            _client = new HttpClient(messageHandler)
            {
                BaseAddress = new Uri("http://not-important.com")
            };
            _stockService = new StockService(_bus, _client, _tokenService, _botConfiguration);
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
            _stockService = new StockService(_bus, _client, _tokenService, _botConfiguration);

            // act
            var result = await _stockService.GetStockAsync("None", CancellationToken.None);

            // assert
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
            _stockService = new StockService(_bus, _client, _tokenService, _botConfiguration);

            var symbol = "TSLA";

            // act
            var result = await _stockService.GetStockAsync(symbol, CancellationToken.None);
            var defaultStock = new StockResponse(symbol, default, default, default, default, default, default, default);

            // assert
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
            _stockService = new StockService(_bus, _client, _tokenService, _botConfiguration);

            var symbol = "AAPL.US";

            // act
            var result = await _stockService.GetStockAsync(symbol, CancellationToken.None);
            var defaultStock = new StockResponse(symbol, DateTime.Parse("2023-03-02"), TimeSpan.Parse("22:00:03"), 144.38M, 146.71M, 143.9M, 145.91M, 52576265);

            // assert
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
        public async Task ShouldReturnCompletedTaskWhenCallEnqueueStockToSearchAsync()
        {
            // arrange

            _httpTest.RespondWith().RespondWith("", 201);
            //_httpTest.CallsTo("*").AllowRealHttp();

            // act
            await _stockService.EnqueueStockToSearchAsync(Stock.Create("", Guid.Empty), CancellationToken.None);

            // assert
            _httpTest.ShouldHaveMadeACall();
        }
    }
}
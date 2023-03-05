using JobSity.Chatroom.Application.Shared.Stocks.Entities;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Stocks.Entities
{
    public class StockTest
    {
        [Fact(DisplayName = "ShouldCreateStock")]
        public void ShouldCreateStock()
        {
            // arrange
            (string ticker, Guid chatRoomId) data =
                ("TSLA", Guid.NewGuid());

            // act
            var stock = new Stock(data.ticker, data.chatRoomId);
            stock.Ticker.Should().Be(data.ticker);
            stock.ChatRoomId.Should().Be(data.chatRoomId);
        }
    }
}

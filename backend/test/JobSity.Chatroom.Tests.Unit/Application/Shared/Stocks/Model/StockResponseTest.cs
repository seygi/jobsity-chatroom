namespace JobSity.Chatroom.Application.Shared.Stocks.Model
{
    public class StockResponseTest
    {
        [Fact(DisplayName = "ShouldCreateStockResponse")]
        public void ShouldCreateStockResponse()
        {
            // arrange
            (string symbol, DateTime date, TimeSpan time, decimal open, decimal high, decimal low, decimal close, int volume) data =
                ("TSLA", DateTime.Now, TimeSpan.Zero, 1, 2, 3, 4, 5);

            // act
            var stock = new StockResponse(data.symbol, data.date, data.time, data.open, data.high, data.low, data.close, data.volume);
            stock.Symbol.Should().Be(data.symbol);
            stock.Date.Should().Be(data.date);
            stock.Time.Should().Be(data.time);
            stock.Open.Should().Be(data.open);
            stock.High.Should().Be(data.high);
            stock.Low.Should().Be(data.low);
            stock.Close.Should().Be(data.close);
            stock.Volume.Should().Be(data.volume);
        }
    }
}

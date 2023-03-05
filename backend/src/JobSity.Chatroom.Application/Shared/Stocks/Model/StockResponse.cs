namespace JobSity.Chatroom.Application.Shared.Stocks.Model
{
    public class StockResponse
    {
        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Volume { get; set; }
        public static StockResponse Empty => new();
    }
}

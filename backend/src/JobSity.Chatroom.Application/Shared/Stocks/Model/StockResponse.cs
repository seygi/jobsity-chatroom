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
        public int Volume { get; set; }
        
        public StockResponse(string symbol, DateTime date, TimeSpan time, decimal open, decimal high, decimal low, decimal close, int volume)
        {
            Symbol = symbol;
            Date = date;
            Time = time;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }
        public static StockResponse Empty => new(default, default, default, default, default, default, default, default);
    }
}

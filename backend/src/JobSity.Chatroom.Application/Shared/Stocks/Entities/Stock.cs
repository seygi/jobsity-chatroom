namespace JobSity.Chatroom.Application.Shared.Stocks.Entities
{
    public class Stock
    {
        public Guid ChatRoomId { get; set; }
        public string Ticker { get; set; }

        public Stock(string ticker, Guid chatRoomId)
        {
            Ticker = ticker;
            ChatRoomId = chatRoomId;
        }

        public static Stock Create(string ticker, Guid chatRoomId)
            => new(ticker, chatRoomId);

    }
}

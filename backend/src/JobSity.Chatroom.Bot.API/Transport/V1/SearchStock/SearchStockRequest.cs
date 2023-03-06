using System.Text.Json.Serialization;

namespace JobSity.Chatroom.Bot.API.Transport.V1.SearchStock
{
    public class SearchStockRequest
    {
        [JsonPropertyName("chatRoomId")]
        public Guid ChatRoomId { get; set; }
        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }

        public SearchStockRequest(Guid chatRoomId, string ticker)
        {
            ChatRoomId = chatRoomId;
            Ticker = ticker;
        }
    }
}

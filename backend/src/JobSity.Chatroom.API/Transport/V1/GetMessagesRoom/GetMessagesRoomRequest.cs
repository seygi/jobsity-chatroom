using System.Text.Json.Serialization;

namespace JobSity.Chatroom.API.Transport.V1.GetMessagesRoom
{
    public class GetMessagesRoomRequest
    {
        [JsonPropertyName("chatRoomId")]
        public Guid ChatRoomId { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace JobSity.Chatroom.API.Transport.V1.GetMessagesRoom
{
    public class GetMessagesRoomRequest
    {
        public GetMessagesRoomRequest(Guid chatRoomId)
        {
            ChatRoomId = chatRoomId;
        }

        [JsonPropertyName("chatRoomId")]
        public Guid ChatRoomId { get; set; }
    }
}

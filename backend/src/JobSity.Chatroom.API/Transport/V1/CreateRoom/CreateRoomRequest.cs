using System.Text.Json.Serialization;

namespace JobSity.Chatroom.API.Transport.V1.CreateRoom
{
    public class CreateRoomRequest
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

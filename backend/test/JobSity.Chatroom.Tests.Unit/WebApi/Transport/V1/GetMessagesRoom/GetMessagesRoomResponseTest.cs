using FluentAssertions;
using System.Text.Json.Serialization;

namespace JobSity.Chatroom.API.Transport.V1.GetMessagesRoom
{
    public class GetMessagesRoomResponseTest
    {

        [JsonPropertyName("createdUserId")]
        public Guid CreatedUserId { get; set; }
        [JsonPropertyName("chatRoomId")]
        public Guid ChatRoomId { get; set; }
        [JsonPropertyName("createdOn")]
        public DateTime CreatedOn { get; set; }
        [JsonPropertyName("createdUserName")]
        public string CreatedUserName { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [Fact(DisplayName = "Should Create Object")]
        public void ShouldCreateObject()
        {
            // arrange - act
            var createdUserId = Guid.NewGuid();
            var chatRoomId = Guid.NewGuid();
            var createdOn = DateTime.Now;
            var createdUserName = "test";
            var text = "test phrase";

            var result = new GetMessagesRoomResponse(createdUserId, chatRoomId, createdOn, createdUserName, text);
            result.CreatedUserId.Should().Be(createdUserId);
            result.ChatRoomId.Should().Be(chatRoomId);
            result.CreatedOn.Should().Be(createdOn);
            result.CreatedUserName.Should().Be(createdUserName);
            result.Text.Should().Be(text);
        }
    }
}

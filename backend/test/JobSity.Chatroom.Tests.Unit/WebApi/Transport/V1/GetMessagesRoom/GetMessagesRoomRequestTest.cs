using FluentAssertions;

namespace JobSity.Chatroom.API.Transport.V1.GetMessagesRoom
{
    public class GetMessagesRoomRequestTest
    {
        [Fact(DisplayName = "Should Create Object")]
        public void ShouldCreateObject()
        {
            // arrange - act
            var chatRoomId = Guid.NewGuid();

            var result = new GetMessagesRoomRequest(chatRoomId);
            result.ChatRoomId.Should().Be(chatRoomId);
        }
    }
}

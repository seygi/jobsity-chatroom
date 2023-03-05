using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase
{
    public class GetMessagesRoomListOutputTest
    {

        [Fact]
        public void CreateGetMessagesRoomListOutput_ShouldCreate_ValidGetMessagesRoomListOutput()
        {
            // Arrange
            var chatRoomId = Guid.NewGuid();
            IEnumerable<ChatMessage> messages = new List<ChatMessage>()
            {
                new ChatMessage(Guid.NewGuid(), chatRoomId, "Test User 1", "Test message 1"),
                new ChatMessage(Guid.NewGuid(), chatRoomId, "Test User 2", "Test message 3")
            };

            // Act
            var output = GetMessagesRoomListOutput.Success(messages);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(2, output.Count());

        }
    }
}
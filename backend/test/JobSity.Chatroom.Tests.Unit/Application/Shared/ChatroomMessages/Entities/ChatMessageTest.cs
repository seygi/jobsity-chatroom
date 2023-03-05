using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.ChatroomMessages.Entities
{
    public class ChatMessageTest
    {
        [Fact]
        public void CreateChatRoom_ShouldCreate_ValidChatMessage()
        {
            // Arrange
            var createdUserId = Guid.NewGuid();
            var chatRoomId = Guid.NewGuid();
            var createdUserName = "Test User";
            var text = "Test message";

            // Act
            var chatRoom = new ChatMessage(createdUserId, chatRoomId, createdUserName, text);

            // Assert
            Assert.NotNull(chatRoom);
            Assert.Equal(createdUserId, chatRoom.CreatedUserId);
            Assert.Equal(chatRoomId, chatRoom.ChatRoomId);
            Assert.Equal(createdUserName, chatRoom.CreatedUserName);
            Assert.Equal(text, chatRoom.Text);
        }
    }
}

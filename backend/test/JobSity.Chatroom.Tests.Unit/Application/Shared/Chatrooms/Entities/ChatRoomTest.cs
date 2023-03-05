using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Chatrooms.Entities
{
    public class ChatRoomTest
    {
        [Fact]
        public void CreateChatRoom_ShouldCreate_ValidChatRoom()
        {
            // Arrange
            var name = "Test Room";
            var createdUserId = Guid.NewGuid();

            // Act
            var chatRoom = new ChatRoom(name, createdUserId);

            // Assert
            Assert.NotNull(chatRoom);
            Assert.Equal(name, chatRoom.Name);
            Assert.Equal(createdUserId, chatRoom.CreatedUserId);
        }
    }
}

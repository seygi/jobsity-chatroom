using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase
{
    public sealed class GetMessagesRoomInputTest
    {
        [Fact]
        public void CreateGetMessagesRoomInput_ShouldCreate_ValidGetMessagesRoomInput()
        {
            // Arrange
            var chatRoomId = Guid.NewGuid();

            // Act
            var input = GetMessagesRoomInput.Create(chatRoomId, null);

            // Assert
            Assert.NotNull(input);
            Assert.Equal(chatRoomId, chatRoomId);
        }
    }
}

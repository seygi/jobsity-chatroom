using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase
{
    public class GetMessagesRoomOutputTest
    {
        [Fact]
        public void CreateGetMessagesRoomOutput_ShouldCreate_ValidGetMessagesRoomOutput()
        {
            // Arrange
            var createdUserId = Guid.NewGuid();
            var chatRoomId = Guid.NewGuid();
            var createdOn = DateTime.UtcNow;
            var createdUserName = "User name";
            var text = "Some text";

            // Act
            var output = GetMessagesRoomOutput.Create(createdUserId, chatRoomId, createdOn, createdUserName, text);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(createdUserId, output.CreatedUserId);
            Assert.Equal(chatRoomId, output.ChatRoomId);
            Assert.Equal(createdOn, output.CreatedOn);
            Assert.Equal(createdUserName, output.CreatedUserName);
            Assert.Equal(text, output.Text);
        }
    }
}
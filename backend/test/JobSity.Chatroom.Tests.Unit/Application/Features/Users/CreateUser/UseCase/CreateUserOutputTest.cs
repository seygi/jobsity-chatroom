using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Users.CreateUser.UseCase
{
    public class CreateUserOutputTest
    {
        [Fact]
        public void CreateCreateUserOutput_ShouldCreate_ValidCreateUserOutput()
        {
            // Arrange
            var success = true;
            var userJwt = "SomeJwt";

            // Act
            var output = CreateUserOutput.Create(userJwt);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(success, output.Success);
            Assert.Equal(userJwt, output.UserJwt);
        }
    }
}

using JobSity.Chatroom.Application.Features.Users.GetUser.UseCase;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Users.GetUser.UseCase
{
    public class GetUserOutputTest
    {
        [Fact]
        public void CreateGetUserOutput_ShouldCreate_ValidGetUserOutput()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            var loginUserOutput = GetUserOutput.Create(userId);

            // Assert
            Assert.NotNull(loginUserOutput);
            Assert.Equal(userId, loginUserOutput.UserId);
        }
    }
}

using JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Users.LoginUser.UseCase
{
    public sealed class LoginUserOutputTest
    {
        [Fact]
        public void CreateLoginUserOutput_ShouldCreate_ValidLoginUserOutput()
        {
            // Arrange
            var userJwt = "SomeJwt";

            // Act
            var loginUserOutput = LoginUserOutput.Create(userJwt);

            // Assert
            Assert.NotNull(loginUserOutput);
            Assert.Equal(userJwt, loginUserOutput.UserJwt);
        }
    }
}

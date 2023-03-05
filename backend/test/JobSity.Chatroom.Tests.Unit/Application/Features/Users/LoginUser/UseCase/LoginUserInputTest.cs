using JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Users.LoginUser.UseCase
{
    public class LoginUserInputTest
    {
        [Fact]
        public void CreateLoginUserInput_ShouldCreate_ValidLoginUserInput()
        {
            // Arrange
            var email = "some@email.com";
            var password = "123456";

            // Act
            var loginUserOutput = LoginUserInput.Create(email, password);

            // Assert
            Assert.NotNull(loginUserOutput);
            Assert.Equal(email, loginUserOutput.Email);
            Assert.Equal(password, loginUserOutput.Password);
        }
    }
}
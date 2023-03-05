using JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Users.CreateUser.UseCase
{
    public class CreateUserInputTest
    {
        [Fact]
        public void CreateCreateUserInput_ShouldCreate_ValidCreateUserInput()
        {
            // Arrange
            var email = "some@email.com";
            var password = "123456";
            var confirmPassword = "123456";

            // Act
            var loginUserOutput = CreateUserInput.Create(email, password, confirmPassword);

            // Assert
            Assert.NotNull(loginUserOutput);
            Assert.Equal(email, loginUserOutput.Email);
            Assert.Equal(password, loginUserOutput.Password);
            Assert.Equal(confirmPassword, loginUserOutput.ConfirmPassword);
        }
    }
}
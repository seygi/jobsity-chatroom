using FluentValidation;
using JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase;
using JobSity.Chatroom.Application.Features.Users.LoginUser.Validators;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Users.LoginUser.Validators
{
    public class LoginUserValidatorTest
    {
        private readonly LoginUserValidator _validator;

        public LoginUserValidatorTest()
        {
            _validator = new LoginUserValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Null_Or_Empty()
        {
            var model = LoginUserInput.Create("", "123456");
            var result = _validator.Validate(model);

            // assert
            result.IsValid.Should().BeFalse();
            result.Errors.Where(x => x.PropertyName == "Email").Count().Should().Be(2);
        }

        [Fact]
        public void Should_Have_Error_When_Password_Is_Null_Or_Empty()
        {
            var model = LoginUserInput.Create("some@email.com", "");
            var result = _validator.Validate(model);

            result.IsValid.Should().BeFalse();
            result.Errors.Where(x => x.PropertyName == "Password").Count().Should().Be(2);
        }

        [Fact]
        public void Should_Return_True_When_Has_All_Correct_Data()
        {
            var model = LoginUserInput.Create("some@email.com", "123456");
            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
            result.Errors.Count().Should().Be(0);
        }
    }
}

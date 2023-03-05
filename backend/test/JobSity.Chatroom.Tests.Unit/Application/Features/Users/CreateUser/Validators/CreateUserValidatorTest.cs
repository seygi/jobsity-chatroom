using FluentValidation;
using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.Validators;
using JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Users.CreateUser.Validators
{
    public class CreateUserValidatorTest
    {
        private readonly CreateUserValidator _validator;

        public CreateUserValidatorTest()
        {
            _validator = new CreateUserValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Email_Is_Null_Or_Empty()
        {
            var model = CreateUserInput.Create("", "123456", "123456");
            var result = _validator.Validate(model);

            // assert
            result.IsValid.Should().BeFalse();
            result.Errors.Where(x => x.PropertyName == "Email").Count().Should().Be(2);
        }

        [Fact]
        public void Should_Return_True_When_Has_All_Correct_Data()
        {
            var model = CreateUserInput.Create("some@email.com", "123456", "123456");
            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
            result.Errors.Count().Should().Be(0);
        }
    }
}

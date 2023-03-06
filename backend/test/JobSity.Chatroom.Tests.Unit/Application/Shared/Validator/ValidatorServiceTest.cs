using FluentValidation;
using FluentValidation.Results;
using JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.Validator;
using NSubstitute;
using System.Net;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Validator
{
    public class ValidatorServiceTest
    {
        private readonly IValidatorService<CreateUserInput> _validatorService;
        private readonly IValidator<CreateUserInput> _validator;
        private readonly INotificationContext _notificationContext;

        private readonly string _email = "some@email.com";
        private readonly string _password = "somepass";
        private readonly string _confirmPassword = "somepass";

        public ValidatorServiceTest()
        {
            _validator = Substitute.For<IValidator<CreateUserInput>>();
            _notificationContext = Substitute.For<INotificationContext>();
            _validatorService = new ValidatorService<CreateUserInput>(_validator, _notificationContext);
        }

        [Fact(DisplayName = "Should return true when input is valid with empty notifications")]
        public void ShouldReturnTrueWhenInputIsValidWithEmptyNotifications()
        {
            // arrange
            var input = new CreateUserInput(_email, _password, _confirmPassword);
            _validator
                .Validate(Arg.Is<CreateUserInput>(t => t.Email == _email && t.Password == _password && t.ConfirmPassword == _confirmPassword))
                .Returns(new ValidationResult());

            // act
            var result = _validatorService.ValidateAndNotifyIfError(input);

            // assert
            result.Should().BeTrue();
            _notificationContext.DidNotReceiveWithAnyArgs().Create(Arg.Any<HttpStatusCode>(), Arg.Any<NotificationErrors>());
            _validator.Received().Validate(Arg.Is<CreateUserInput>(t => t.Email == _email && t.Password == _password && t.ConfirmPassword == _confirmPassword));
        }

        [Fact(DisplayName = "Should Return False When Input Invalid With Notifications")]
        public void ShouldReturnFalseWhenInputInvalidWithNotifications()
        {
            // arrange
            var input = new CreateUserInput(_email, _password, _confirmPassword);
            _validator
                .Validate(Arg.Is<CreateUserInput>(t => t.Email == _email && t.Password == _password && t.ConfirmPassword == _confirmPassword))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                    new("Email", "FailEmail")
                }));

            var notificationErrorsExpected = NotificationErrors.Empty;
            notificationErrorsExpected.Add("Email", "FailEmail");

            Func<object, bool> validateContentParamNotificationContext = notificationResult =>
            {
                notificationResult.Should().BeEquivalentTo(notificationErrorsExpected);
                return true;
            };

            // act
            var result = _validatorService.ValidateAndNotifyIfError(input);

            // assert
            result.Should().BeFalse();
            _notificationContext.Received().Create(Arg.Is<HttpStatusCode>(t => t == HttpStatusCode.BadRequest), Arg.Is<NotificationErrors>(t => validateContentParamNotificationContext(t)));
            _validator.Received().Validate(Arg.Is<CreateUserInput>(t => t.Email == _email && t.Password == _password && t.ConfirmPassword == _confirmPassword));

        }

        [Fact(DisplayName = "Should Return True When Input Is Valid With Empty Validation Erros")]
        public void ShouldReturnTrueWhenInputIsValidWithEmptyValidationErros()
        {
            // arrange
            var input = new CreateUserInput(_email, _password, _confirmPassword);
            _validator
                .Validate(Arg.Is<CreateUserInput>(t => t.Email == _email && t.Password == _password && t.ConfirmPassword == _confirmPassword))
                .Returns(new ValidationResult());

            // act
            var result = _validatorService.Validate(input, out var notificationErrors);

            // assert
            result.Should().BeTrue();
            notificationErrors.Should().BeEquivalentTo(NotificationErrors.Empty);
            _validator.Received().Validate(Arg.Is<CreateUserInput>(t => t.Email == _email && t.Password == _password && t.ConfirmPassword == _confirmPassword));
            _notificationContext.DidNotReceiveWithAnyArgs().Create(Arg.Any<HttpStatusCode>(), Arg.Any<NotificationErrors>());
        }

        [Fact(DisplayName = "Should Return False When Input Invalid With Validation Errors")]
        public void ShouldReturnFalseWhenInputInvalidWithValidationErrors()
        {
            // arrange
            var input = new CreateUserInput(_email, _password, _confirmPassword);
            _validator
                .Validate(Arg.Is<CreateUserInput>(t => t.Email == _email && t.Password == _password && t.ConfirmPassword == _confirmPassword))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                    new("Email", "FailEmail")
                }));

            var notificationErrorsExpected = NotificationErrors.Empty;
            notificationErrorsExpected.Add("Email", "FailEmail");

            // act
            var result = _validatorService.Validate(input, out var notificationErrors);

            // assert
            result.Should().BeFalse();
            notificationErrors.Should().BeEquivalentTo(notificationErrorsExpected);
            _validator.Received().Validate(Arg.Is<CreateUserInput>(t => t.Email == _email && t.Password == _password && t.ConfirmPassword == _confirmPassword));
            _notificationContext.Received(1).Create(HttpStatusCode.BadRequest, notificationErrors);
        }
    }
}
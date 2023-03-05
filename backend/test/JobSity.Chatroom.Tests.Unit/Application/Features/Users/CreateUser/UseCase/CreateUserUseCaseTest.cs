using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;
using NSubstitute;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Users.CreateUser.UseCase
{
    public class CreateUserUseCaseTest
    {
        private readonly IValidatorService<CreateUserInput> _validatorService;
        private readonly INotificationContext _notificationContext;
        private readonly IOptions<AppJwtSettings> _appJwtSettings;
        private readonly UserManager<IdentityUser> _userManager;


        private readonly CreateUserUseCase _createUserUseCase;

        public CreateUserUseCaseTest()
        {
            _validatorService = Substitute.For<IValidatorService<CreateUserInput>>();
            _notificationContext = Substitute.For<INotificationContext>();
            _appJwtSettings = Options.Create(new AppJwtSettings());
            _userManager = Substitute.For<UserManager<IdentityUser>>(
                Substitute.For<IUserStore<IdentityUser>>(),
                Substitute.For<IOptions<IdentityOptions>>(),
                Substitute.For<IPasswordHasher<IdentityUser>>(),
                new IUserValidator<IdentityUser>[0],
                new IPasswordValidator<IdentityUser>[0],
                Substitute.For<ILookupNormalizer>(),
                Substitute.For<IdentityErrorDescriber>(),
                Substitute.For<IServiceProvider>(),
                Substitute.For<ILogger<UserManager<IdentityUser>>>()
            );

            _createUserUseCase = new CreateUserUseCase(
                _validatorService,
                _notificationContext,
                _appJwtSettings, 
                _userManager);
        }

        [Fact]
        public async Task HandleAsync_InvalidInput_ReturnsEmpty()
        {
            // Arrange
            var email = "some@email.com";
            var password = "123456";
            var confirmPassword = "123456";
            var request = CreateUserInput.Create(email, password, confirmPassword);

            _validatorService.ValidateAndNotifyIfError(Arg.Any<CreateUserInput>()).Returns(false);

            // Act
            var response = await _createUserUseCase.ExecuteAsync(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(CreateUserOutput.Empty);
        }

        [Fact]
        public async Task HandleAsync_ValidInput_ReturnsJWT()
        {
            // Arrange
            var email = "some@email.com";
            var password = "123456";
            var confirmPassword = "123456";
            var request = CreateUserInput.Create(email, password, confirmPassword);

            _validatorService.ValidateAndNotifyIfError(Arg.Any<CreateUserInput>()).Returns(true);
            _userManager.CreateAsync(Arg.Any<IdentityUser>(), Arg.Any<string>()).Returns(Task.FromResult(IdentityResult.Success));
            _appJwtSettings.Value.SecretKey = "my_super_secret_key";

            // Act
            var response = await _createUserUseCase.ExecuteAsync(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            Assert.True(response.Success);
            response.UserJwt.Should().NotBeNull();
        }
    }
}

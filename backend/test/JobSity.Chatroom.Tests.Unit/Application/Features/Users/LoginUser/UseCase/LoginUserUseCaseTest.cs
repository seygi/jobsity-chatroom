using JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.Validator;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;
using NSubstitute;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Users.LoginUser.UseCase
{
    public class LoginUserUseCaseTest
    {
        private readonly IValidatorService<LoginUserInput> _validatorService;
        private readonly INotificationContext _notificationContext;
        private readonly IOptions<AppJwtSettings> _appJwtSettings;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginUserUseCaseTest()
        {
            _validatorService = Substitute.For<IValidatorService<LoginUserInput>>();
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
            _signInManager = new SignInManager<IdentityUser>(_userManager,
                 new HttpContextAccessor(),
                 Substitute.For<IUserClaimsPrincipalFactory<IdentityUser>>(),
                 Substitute.For<IOptions<IdentityOptions>>(),
                 Substitute.For<ILogger<SignInManager<IdentityUser>>>(),
                 Substitute.For<IAuthenticationSchemeProvider>(),
                 Substitute.For<IUserConfirmation<IdentityUser>>());
        }

        [Fact]
        public async Task HandleAsync_InvalidInput_ReturnsEmpty()
        {
            // Arrange
            var request = LoginUserInput.Create("some@email.com", "123456");

            _validatorService.ValidateAndNotifyIfError(Arg.Any<LoginUserInput>()).Returns(true);

            var user = new IdentityUser { Email = request.Email };
            _userManager.FindByEmailAsync(request.Email).Returns(user);
            _userManager.CheckPasswordAsync(user, request.Password).Returns(true);
            _userManager.GetRolesAsync(user).Returns(new[] { "UserRole" });
            _appJwtSettings.Value.SecretKey = "my_super_secret_key";

            var useCase = new LoginUserUseCase(
                _validatorService,
                Substitute.For<INotificationContext>(),
                _appJwtSettings,
                _userManager,
                _signInManager
            );

            // Act
            var response = await useCase.ExecuteAsync(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(LoginUserOutput.Empty);
        }
    }
}

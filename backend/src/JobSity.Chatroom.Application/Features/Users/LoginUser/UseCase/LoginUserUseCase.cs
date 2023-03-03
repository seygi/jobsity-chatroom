using JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase;
using JobSity.Chatroom.Application.Shared.Configurations;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.UseCase;
using JobSity.Chatroom.Application.Shared.Users.UseCases;
using JobSity.Chatroom.Application.Shared.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;
using NetDevPack.Identity.Model;
using System.Net;

namespace JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase
{
    internal sealed class LoginUserUseCase : UserUseCase, IUseCase<LoginUserInput, LoginUserOutput>
    {
        private readonly IValidatorService<LoginUserInput> _validatorService;
        private readonly INotificationContext _notificationContext;
        private readonly SignInManager<IdentityUser> _signInManager;

        public LoginUserUseCase(IValidatorService<LoginUserInput> validatorService, INotificationContext notificationContext, IOptions<AppJwtSettings> appJwtSettings, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
            : base(appJwtSettings, userManager)
        {
            _validatorService = validatorService;
            _notificationContext = notificationContext;
            _signInManager = signInManager;
        }

        public async Task<LoginUserOutput> ExecuteAsync(LoginUserInput input, CancellationToken cancellationToken)
        {
            if (!_validatorService.ValidateAndNotifyIfError(input))
                return LoginUserOutput.Empty;

            var result = await _signInManager.PasswordSignInAsync(input.Email, input.Password, false, false);

            if (result.IsLockedOut)
            {
                _notificationContext.Create(HttpStatusCode.Forbidden, "User is locked out.");
                return LoginUserOutput.Empty;
            }
            if (result.IsNotAllowed)
            {
                _notificationContext.Create(HttpStatusCode.Forbidden, "User is not allowed to login.");
                return LoginUserOutput.Empty;
            }
            if (result.RequiresTwoFactor)
            {
                _notificationContext.Create(HttpStatusCode.Forbidden, "You must inform the 2fa.");
                return LoginUserOutput.Empty;
            }
            if (!result.Succeeded)
            {
                _notificationContext.Create(HttpStatusCode.Unauthorized, "Username or password incorrect.");
                return LoginUserOutput.Empty;
            }

            var userJwt = GetFullJwt(input.Email);

            return LoginUserOutput.Create(userJwt);
        }
    }
}

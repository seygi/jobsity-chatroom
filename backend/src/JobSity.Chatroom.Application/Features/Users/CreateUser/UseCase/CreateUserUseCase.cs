using JobSity.Chatroom.Application.Features.Users.CreateUser.UseCase;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.UseCase;
using JobSity.Chatroom.Application.Shared.Users.UseCases;
using JobSity.Chatroom.Application.Shared.Validator;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using NetDevPack.Identity.Jwt;
using System.Net;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase
{
    internal sealed class CreateUserUseCase : UserUseCaseBase, IUseCase<CreateUserInput, CreateUserOutput>
    {
        private readonly IValidatorService<CreateUserInput> _validatorService;
        private readonly INotificationContext _notificationContext;

        public CreateUserUseCase(IValidatorService<CreateUserInput> validatorService, INotificationContext notificationContext, IOptions<AppJwtSettings> appJwtSettings, UserManager<IdentityUser> userManager)
            : base(appJwtSettings, userManager)
        {
            _validatorService = validatorService;
            _notificationContext = notificationContext;
        }

        public async Task<CreateUserOutput> ExecuteAsync(CreateUserInput input, CancellationToken cancellationToken)
        {
            if (!_validatorService.ValidateAndNotifyIfError(input))
                return CreateUserOutput.Empty;

            var user = new IdentityUser
            {
                UserName = input.Email,
                Email = input.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, input.Password);

            if (!result.Succeeded)
            {
                _notificationContext.Create(HttpStatusCode.InternalServerError, "Error on create user, please try again.");
                return CreateUserOutput.Empty;
            }

            var userJwt = GetFullJwt(user.Email);

            return CreateUserOutput.Create(userJwt);
        }
    }
}

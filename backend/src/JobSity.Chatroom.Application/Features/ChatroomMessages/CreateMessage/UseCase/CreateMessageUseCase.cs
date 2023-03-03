using JobSity.Chatroom.Application.Shared.ChatroomMessages.Services;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.UseCase;
using JobSity.Chatroom.Application.Shared.Validator;
using System.Net;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase
{
    internal sealed class CreateMessageUseCase : IUseCase<CreateMessageInput, CreateMessageOutput>
    {
        private readonly IValidatorService<CreateMessageInput> _validatorService;
        private readonly INotificationContext _notificationContext;
        private readonly IChatMessageService _chatMessageService;

        public CreateMessageUseCase(IValidatorService<CreateMessageInput> validatorService, INotificationContext notificationContext, IChatMessageService chatMessageService)
        {
            _validatorService = validatorService;
            _notificationContext = notificationContext;
            _chatMessageService = chatMessageService;
        }

        public async Task<CreateMessageOutput> ExecuteAsync(CreateMessageInput input, CancellationToken cancellationToken)
        {
            if (!_validatorService.ValidateAndNotifyIfError(input))
                return CreateMessageOutput.Empty;

            var rowsAffected = await _chatMessageService.CreateMessageAsync(input, cancellationToken);

            if (rowsAffected <= 0)
            {
                _notificationContext.Create(HttpStatusCode.InternalServerError, "Error on create message, please try again.");
                return CreateMessageOutput.Empty;
            }
            return CreateMessageOutput.Create(true);
        }
    }
}

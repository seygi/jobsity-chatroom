using JobSity.Chatroom.Application.Shared.ChatroomMessages.Services;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.UseCase;
using JobSity.Chatroom.Application.Shared.Validator;
using System.Net;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase
{
    internal sealed class GetMessagesRoomUseCase : IUseCase<GetMessagesRoomInput, GetMessagesRoomListOutput>
    {
        private readonly IValidatorService<GetMessagesRoomInput> _validatorService;
        private readonly INotificationContext _notificationContext;
        private readonly IChatMessageService _chatMessageService;

        public GetMessagesRoomUseCase(IValidatorService<GetMessagesRoomInput> validatorService, INotificationContext notificationContext, IChatMessageService chatMessageService)
        {
            _validatorService = validatorService;
            _notificationContext = notificationContext;
            _chatMessageService = chatMessageService;
        }

        public async Task<GetMessagesRoomListOutput> ExecuteAsync(GetMessagesRoomInput input, CancellationToken cancellationToken)
        {
            if (!_validatorService.ValidateAndNotifyIfError(input))
                return GetMessagesRoomListOutput.Empty;

            var messages = await _chatMessageService.GetAllByChatRoomId(input.ChatRoomId, cancellationToken);

            if (messages.Any())
                return GetMessagesRoomListOutput.Success(messages);

            _notificationContext.Create(HttpStatusCode.NotFound);
            return GetMessagesRoomListOutput.Empty;
        }
    }
}
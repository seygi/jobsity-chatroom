using JobSity.Chatroom.Application.Shared.Chatrooms.Services;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.UseCase;
using JobSity.Chatroom.Application.Shared.Validator;
using NetDevPack.Identity.User;
using System.Net;

namespace JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.UseCase
{
    internal sealed class CreateRoomUseCase : IUseCase<CreateRoomInput, CreateRoomOutput>
    {
        private readonly IValidatorService<CreateRoomInput> _validatorService;
        private readonly INotificationContext _notificationContext;
        private readonly IChatRoomService _chatRoomService;
        private readonly IAspNetUser _aspNetUser;

        public CreateRoomUseCase(IValidatorService<CreateRoomInput> validatorService, INotificationContext notificationContext, IChatRoomService chatRoomService, IAspNetUser aspNetUser)
        {
            _validatorService = validatorService;
            _notificationContext = notificationContext;
            _chatRoomService = chatRoomService;
            _aspNetUser = aspNetUser;
        }

        public async Task<CreateRoomOutput> ExecuteAsync(CreateRoomInput input, CancellationToken cancellationToken)
        {
            if (!_validatorService.ValidateAndNotifyIfError(input))
                return CreateRoomOutput.Empty;

            input.CreatedUserId = _aspNetUser.GetUserId();

            var rowsAffected = await _chatRoomService.CreateChatRoomAsync(input, cancellationToken);

            if (rowsAffected <= 0)
            {
                _notificationContext.Create(HttpStatusCode.InternalServerError, "Error on create room, please try again.");
                return CreateRoomOutput.Empty;
            }
            return CreateRoomOutput.Create(true);
        }
    }
}

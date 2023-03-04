using JobSity.Chatroom.Application.Shared.Chatrooms.Services;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.UseCase;
using System.Net;

namespace JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.UseCase
{
    internal sealed class GetAllRoomsUseCase : IUseCase<DefaultInput, GetAllRoomsListOutput>
    {
        private readonly INotificationContext _notificationContext;
        private readonly IChatRoomService _chatRoomService;

        public GetAllRoomsUseCase(INotificationContext notificationContext, IChatRoomService chatRoomService)
        {
            _notificationContext = notificationContext;
            _chatRoomService = chatRoomService;
        }

        public async Task<GetAllRoomsListOutput> ExecuteAsync(DefaultInput input, CancellationToken cancellationToken)
        {
            var rooms = await _chatRoomService.GetAllAsync(cancellationToken);

            if (rooms.Any())
                return GetAllRoomsListOutput.Success(rooms);

            _notificationContext.Create(HttpStatusCode.NotFound);
            return GetAllRoomsListOutput.Empty;
        }
    }
}
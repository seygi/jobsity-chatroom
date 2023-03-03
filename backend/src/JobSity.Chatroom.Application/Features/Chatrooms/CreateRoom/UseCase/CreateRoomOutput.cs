using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.UseCase
{
    public sealed class CreateRoomOutput : IOutput
    {
        public bool Success { get; }

        private CreateRoomOutput(bool success)
        {
            Success = success;
        }

        public static CreateRoomOutput Create(bool success) => new(success);

        public static CreateRoomOutput Empty => new(false);
    }
}

using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase
{
    public sealed class CreateUserOutput : IOutput
    {
        public bool Success { get; }
        public string UserJwt { get; }

        private CreateUserOutput(bool success, string userJwt)
        {
            Success = success;
            UserJwt = userJwt;
        }

        public static CreateUserOutput Create(string userJwt) => new(true, userJwt);

        public static CreateUserOutput Empty => new(false, default);
    }
}

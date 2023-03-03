using JobSity.Chatroom.Application.Shared.UseCase;

namespace JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase
{
    public sealed class LoginUserOutput : IOutput
    {
        public bool Success { get; }
        public string UserJwt { get; }

        private LoginUserOutput(bool success, string userJwt)
        {
            Success = success;
            UserJwt = userJwt;
        }

        public static LoginUserOutput Create(string userJwt) => new(true, userJwt);

        public static LoginUserOutput Empty => new(false, default);
    }
}

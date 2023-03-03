using JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase;

namespace JobSity.Chatroom.API.Transport.V1.CreateUser
{
    public sealed class LoginUserResponse
    {
        public string UserJwt { get; set; }
        public LoginUserResponse(string userJwt)
        {
            UserJwt = userJwt;
        }
        public static LoginUserResponse Create(LoginUserOutput outputUseCase)
            => new(outputUseCase.UserJwt);
    }
}
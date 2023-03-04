using JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase;
using System.Text.Json.Serialization;

namespace JobSity.Chatroom.API.Transport.V1.LoginUser
{
    public sealed class LoginUserResponse
    {
        [JsonPropertyName("userJwt")]
        public string UserJwt { get; set; }
        public LoginUserResponse(string userJwt)
        {
            UserJwt = userJwt;
        }
        public static LoginUserResponse Create(LoginUserOutput outputUseCase)
            => new(outputUseCase.UserJwt);
    }
}
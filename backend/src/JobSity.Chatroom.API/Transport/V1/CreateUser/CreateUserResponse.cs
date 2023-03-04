using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using System.Text.Json.Serialization;

namespace JobSity.Chatroom.API.Transport.V1.CreateUser
{
    public sealed class CreateUserResponse
    {
        [JsonPropertyName("userJwt")]
        public string UserJwt { get; set; }
        public CreateUserResponse(string userJwt)
        {
            UserJwt = userJwt;
        }
        public static CreateUserResponse Create(CreateUserOutput outputUseCase)
            => new(outputUseCase.UserJwt);
    }
}
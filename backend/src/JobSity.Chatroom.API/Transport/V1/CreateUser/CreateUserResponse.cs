using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Features.Users.LoginUser.UseCase;

namespace JobSity.Chatroom.API.Transport.V1.CreateUser
{
    public sealed class CreateUserResponse
    {
        public string UserJwt { get; set; }
        public CreateUserResponse(string userJwt)
        {
            UserJwt = userJwt;
        }
        public static CreateUserResponse Create(CreateUserOutput outputUseCase)
            => new(outputUseCase.UserJwt);
    }
}
using JobSity.Chatroom.Application.Features.Users.GetUser.UseCase;
using System.Text.Json.Serialization;

namespace JobSity.Chatroom.API.Transport.V1.GetUser
{
    public class GetUserResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        public GetUserResponse(Guid id)
        {
            Id = id;
        }
        public static GetUserResponse Create(GetUserOutput outputUseCase)
            => new(outputUseCase.UserId);
    }
}

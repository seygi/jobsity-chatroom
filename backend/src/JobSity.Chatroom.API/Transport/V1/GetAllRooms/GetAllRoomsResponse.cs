using JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.UseCase;
using System.Text.Json.Serialization;

namespace JobSity.Chatroom.API.Transport.V1.GetAllRooms
{
    public sealed class GetAllRoomsResponse
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        public GetAllRoomsResponse(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public static IList<GetAllRoomsResponse> Create(GetAllRoomsListOutput outputUseCase) =>
            outputUseCase.Select(lnq => new GetAllRoomsResponse(lnq.Id, lnq.Name))
               .ToList();
    }
}
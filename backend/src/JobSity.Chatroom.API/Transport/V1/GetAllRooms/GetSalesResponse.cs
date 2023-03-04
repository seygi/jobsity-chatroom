using JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.UseCase;
using System.Text.Json.Serialization;

namespace Hubla.Sales.API.Transport.V1.GetSales
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
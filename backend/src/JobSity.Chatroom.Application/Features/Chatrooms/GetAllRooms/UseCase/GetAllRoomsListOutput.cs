using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using JobSity.Chatroom.Application.Shared.UseCase;
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.UseCase
{
    [ExcludeFromCodeCoverage]
    public sealed class GetAllRoomsListOutput : IOutput, IEnumerable<GetAllRoomsOutput>
    {
        private readonly IList<GetAllRoomsOutput> _roomsOutput;

        private GetAllRoomsListOutput(IEnumerable<ChatRoom> rooms)
        {
            _roomsOutput = new List<GetAllRoomsOutput>();
            foreach (var room in rooms)
                _roomsOutput.Add(GetAllRoomsOutput.Create(room.Id, room.Name));
        }

        public static GetAllRoomsListOutput Create(IEnumerable<ChatRoom> rooms) => new(rooms ?? Array.Empty<ChatRoom>());

        public IEnumerator<GetAllRoomsOutput> GetEnumerator()
        {
            foreach (var room in _roomsOutput)
                yield return room;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static GetAllRoomsListOutput Success(IEnumerable<ChatRoom> rooms) => new(rooms);

        public static GetAllRoomsListOutput Empty => new(Array.Empty<ChatRoom>());
    }
}
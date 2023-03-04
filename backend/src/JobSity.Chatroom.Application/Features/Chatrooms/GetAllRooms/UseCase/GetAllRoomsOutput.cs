namespace JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.UseCase
{
    public class GetAllRoomsOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        private GetAllRoomsOutput(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public static GetAllRoomsOutput Create(Guid id, string name) =>
            new(id, name);
    }
}
using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Shared.Chatrooms.UseCases.Inputs
{
    [ExcludeFromCodeCoverage]
    public abstract class CreateRoomInputBase
    {
        public string Name { get; set; }
        public Guid CreatedUserId { get; set; }

        public CreateRoomInputBase(string name, Guid createdUserId)
        {
            Name = name;
            CreatedUserId = createdUserId;
        }
    }
}
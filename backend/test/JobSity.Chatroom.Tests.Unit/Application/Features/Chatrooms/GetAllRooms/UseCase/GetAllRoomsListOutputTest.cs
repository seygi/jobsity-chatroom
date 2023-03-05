using JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.UseCase;
using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Chatrooms.GetAllRooms.UseCase
{
    public sealed class GetAllRoomsListOutputTest
    {

        [Fact]
        public void CreateGetAllRoomsList_ShouldCreate_ValidGetAllRoomsList()
        {
            // Arrange
            IEnumerable<ChatRoom> rooms = new List<ChatRoom>()
            {
                new ChatRoom("Test Room 1", Guid.NewGuid()),
                new ChatRoom("Test Room 2", Guid.NewGuid())
            };

            // Act
            var output = GetAllRoomsListOutput.Success(rooms);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(2, output.Count());

        }
    }
}
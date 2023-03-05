using JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.UseCase;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Chatrooms.GetAllRooms.UseCase
{
    public class GetAllRoomsOutputTest
    {
        [Fact]
        public void CreateGetAllRoomsOutput_ShouldCreate_ValidGetAllRoomsOutput()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Some Room";

            // Act
            var output = GetAllRoomsOutput.Create(id, name);

            // Assert
            Assert.NotNull(output);
            Assert.Equal(id, output.Id);
            Assert.Equal(name, output.Name);
        }
    }
}
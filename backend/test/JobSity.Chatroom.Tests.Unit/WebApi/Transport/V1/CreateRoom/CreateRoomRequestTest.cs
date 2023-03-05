using JobSity.Chatroom.API.Transport.V1.CreateRoom;
using System.Text.Json.Serialization;

namespace JobSity.Chatroom.Tests.Unit.WebApi.Transport.V1.CreateRoom
{
    public class CreateRoomRequestTest
    {
        [Theory(DisplayName = "Should Create Object")]
        [InlineData(default)]
        [InlineData("A")]
        [InlineData("Z")]
        public void ShouldCreateObject(string name)
        {
            // arrange - act
            var result = new CreateRoomRequest(name);
            result.Name.Should().Be(name);


            // assert
            AssertProperties(result, name);
        }

        private static void AssertProperties(CreateRoomRequest result, string name)
        {
            result.Name.ShouldBe().BeEquivalentTo(name);
        }
    }
}

namespace JobSity.Chatroom.API.Transport.V1.GetAllRooms
{
    public sealed class GetAllRoomsResponseTest
    {
        [Theory(DisplayName = "Should Create Object")]
        [InlineData(default)]
        [InlineData("A")]
        [InlineData("Z")]
        public void ShouldCreateObject(string name)
        {
            // arrange - act
            var guid = Guid.NewGuid();

            var result = new GetAllRoomsResponse(guid, name);
            result.Id.Should().Be(guid);
            result.Name.Should().Be(name);
        }
    }
}
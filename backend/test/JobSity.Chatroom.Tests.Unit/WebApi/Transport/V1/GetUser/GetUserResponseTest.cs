namespace JobSity.Chatroom.API.Transport.V1.GetUser
{
    public class GetUserResponseTest
    {
        [Fact(DisplayName = "Should Create Object")]
        public void ShouldCreateObject()
        {
            // arrange - act
            var id = Guid.NewGuid();

            var result = new GetUserResponse(id);
            result.Id.Should().Be(id);
        }
    }
}

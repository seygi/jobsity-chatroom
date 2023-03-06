using JobSity.Chatroom.API.Transport.V1.CreateUser;

namespace JobSity.Chatroom.Tests.Unit.WebApi.Transport.V1.CreateUser
{
    public sealed class CreateUserResponseTest
    {
        [Theory(DisplayName = "Should Create Object")]
        [InlineData(default)]
        [InlineData("A")]
        [InlineData("Z")]
        public void ShouldCreateObject(string userJwt)
        {
            // arrange - act
            var result = new CreateUserResponse(userJwt);
            result.UserJwt.Should().Be(userJwt);
        }
    }
}
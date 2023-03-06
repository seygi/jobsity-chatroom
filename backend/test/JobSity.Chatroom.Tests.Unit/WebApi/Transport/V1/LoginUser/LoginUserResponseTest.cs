namespace JobSity.Chatroom.API.Transport.V1.LoginUser
{
    public sealed class LoginUserResponseTest
    {
        [Theory(DisplayName = "Should Create Object")]
        [InlineData(default)]
        [InlineData("A")]
        [InlineData("Z")]
        public void ShouldCreateObject(string userJwt)
        {
            var result = new LoginUserResponse(userJwt);
            result.UserJwt.Should().Be(userJwt);
        }
    }
}
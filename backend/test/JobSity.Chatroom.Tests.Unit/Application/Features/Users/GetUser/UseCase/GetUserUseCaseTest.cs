using JobSity.Chatroom.Application.Features.Users.GetUser.UseCase;
using JobSity.Chatroom.Application.Shared.UseCase;
using NetDevPack.Identity.User;
using NSubstitute;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Users.GetUser.UseCase
{
    public class GetUserUseCaseTest
    {
        private readonly IAspNetUser _aspNetUser;
        private readonly GetUserUseCase _getUserUseCase;

        public GetUserUseCaseTest()
        {
            _aspNetUser = Substitute.For<IAspNetUser>();
            _getUserUseCase = new GetUserUseCase(_aspNetUser);
        }

        [Fact]
        public async Task Handle_ValidUserId_ReturnsUserId()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _aspNetUser.GetUserId().Returns(userId);

            // Act
            var result = await _getUserUseCase.ExecuteAsync(DefaultInput.Default, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.UserId.Should().Be(userId);
        }

        [Fact]
        public async Task Handle_InvalidUserId_ReturnsNull()
        {
            // Arrange
            _aspNetUser.GetUserId().Returns(Guid.Empty);

            // Act
            var result = await _getUserUseCase.ExecuteAsync(DefaultInput.Default, CancellationToken.None);

            // Assert
            result.UserId.Should().Be(Guid.Empty);
        }
    }
}

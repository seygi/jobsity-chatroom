using JobSity.Chatroom.Application.Features.Chatrooms.GetAllRooms.UseCase;
using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using JobSity.Chatroom.Application.Shared.Chatrooms.Services;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.UseCase;
using NSubstitute;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.Chatrooms.GetAllRooms.UseCase
{
    public class GetAllRoomsUseCaseTest
    {
        private readonly INotificationContext _notificationContext;
        private readonly IChatRoomService _chatRoomService;
        private readonly GetAllRoomsUseCase _useCase;

        public GetAllRoomsUseCaseTest()
        {
            _notificationContext = Substitute.For<INotificationContext>();
            _chatRoomService = Substitute.For<IChatRoomService>();
            _useCase = new GetAllRoomsUseCase(_notificationContext, _chatRoomService);
        }

        [Fact]
        public async Task Handle_ReturnsAllChatrooms_WhenRoomsExist()
        {
            // Arrange
            var expectedRooms = new List<ChatRoom>
            {
                new ChatRoom("Test Room 1", Guid.NewGuid()),
                new ChatRoom("Test Room 2", Guid.NewGuid())
            };

            _chatRoomService.GetAllAsync(Arg.Any<CancellationToken>()).Returns(expectedRooms);

            // Act
            var result = await _useCase.ExecuteAsync(DefaultInput.Default, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(expectedRooms.Count);
        }

        [Fact]
        public async Task Handle_ReturnsEmptyList_WhenNoRoomsExist()
        {
            // Arrange
            var expectedRooms = new List<ChatRoom>();
            _chatRoomService.GetAllAsync(Arg.Any<CancellationToken>()).Returns(expectedRooms);

            // Act
            var result = await _useCase.ExecuteAsync(DefaultInput.Default, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeEmpty();
        }
    }
}
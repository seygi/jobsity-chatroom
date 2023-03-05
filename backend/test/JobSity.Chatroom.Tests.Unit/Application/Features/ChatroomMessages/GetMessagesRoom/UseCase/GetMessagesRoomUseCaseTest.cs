using JobSity.Chatroom.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Services;
using JobSity.Chatroom.Application.Shared.Notifications;
using JobSity.Chatroom.Application.Shared.Validator;
using NSubstitute;

namespace JobSity.Chatroom.Tests.Unit.Application.Features.ChatroomMessages.GetMessagesRoom.UseCase
{
    public class GetMessagesRoomUseCaseTest
    {
        private readonly IValidatorService<GetMessagesRoomInput> _validatorService;
        private readonly INotificationContext _notificationContext;
        private readonly IChatMessageService _chatMessageService;
        private readonly GetMessagesRoomUseCase _usecase;

        public GetMessagesRoomUseCaseTest()
        {
            _validatorService = Substitute.For<IValidatorService<GetMessagesRoomInput>>();
            _notificationContext = Substitute.For<INotificationContext>();
            _chatMessageService = Substitute.For<IChatMessageService>();

            _usecase = new GetMessagesRoomUseCase(_validatorService, _notificationContext, _chatMessageService);
        }

        [Fact]
        public async Task HandleAsync_WithValidRoomId_ReturnsMessages()
        {
            // Arrange
            var chatRoomId = Guid.NewGuid();
            IEnumerable<ChatMessage> messages = new List<ChatMessage>()
            {
                new ChatMessage(Guid.NewGuid(), chatRoomId, "Test User 1", "Test message 1"),
                new ChatMessage(Guid.NewGuid(), chatRoomId, "Test User 2", "Test message 2"),
                new ChatMessage(Guid.NewGuid(), chatRoomId, "Test User 3", "Test message 3")
            };
            _validatorService.ValidateAndNotifyIfError(Arg.Any<GetMessagesRoomInput>()).Returns(true);
            _chatMessageService.GetTop50ByChatRoomId(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(messages);

            // Act
            var result = await _usecase.ExecuteAsync(GetMessagesRoomInput.Create(chatRoomId, null), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(messages.Count(), result.Count());


            foreach (var message in messages)
            {
                var item = result.FirstOrDefault(x => x.CreatedUserId == message.CreatedUserId);
                Assert.Equal(message.CreatedUserId, item.CreatedUserId);
                Assert.Equal(message.ChatRoomId, item.ChatRoomId);
                Assert.Equal(message.CreatedUserName, item.CreatedUserName);
                Assert.Equal(message.Text, item.Text);
            }
        }

        [Fact]
        public async Task HandleAsync_WithInvalidRoomId_ReturnsEmptyList()
        {
            // Arrange
            var chatRoomId = Guid.NewGuid();
            _chatMessageService.GetTop50ByChatRoomId(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(new List<ChatMessage>());

            // Act
            var result = await _usecase.ExecuteAsync(GetMessagesRoomInput.Create(chatRoomId, null), CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
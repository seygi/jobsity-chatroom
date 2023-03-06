using JobSity.Chatroom.Application.Features.ChatroomMessages.CreateMessage.UseCase;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories;
using JobSity.Chatroom.Application.Shared.Messaging;
using JobSity.Chatroom.Application.Shared.Stocks.Entities;
using NSubstitute;

namespace JobSity.Chatroom.Application.Shared.ChatroomMessages.Services
{
    public class ChatMessageServiceTest
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        private readonly IBus _bus;
        private readonly IChatMessageService _chatMessageService;

        public ChatMessageServiceTest()
        {
            _chatMessageRepository = Substitute.For<IChatMessageRepository>();
            _bus = Substitute.For<IBus>();
            _chatMessageService = new ChatMessageService(_chatMessageRepository, _bus);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllMessages()
        {
            // Arrange
            var chatRoomId = Guid.NewGuid();
            var expectedRooms = new List<ChatMessage>
            {
                new ChatMessage(Guid.NewGuid(), chatRoomId, "Test User 1", "Test message 1"),
                new ChatMessage(Guid.NewGuid(), chatRoomId, "Test User 2", "Test message 2"),
                new ChatMessage(Guid.NewGuid(), chatRoomId, "Test User 3", "Test message 3")
            };

            _chatMessageRepository.GetTop50ByChatRoomId(Arg.Any<Guid>()).Returns(expectedRooms);

            // Act
            var actualMessages = (await _chatMessageService.GetTop50ByChatRoomId(chatRoomId, CancellationToken.None)).ToArray();

            // Assert
            Assert.Equal(expectedRooms.Count, actualMessages.Length);
            for (int i = 0; i < expectedRooms.Count; i++)
            {
                Assert.Equal(expectedRooms[i].Id, actualMessages[i].Id);
                Assert.Equal(expectedRooms[i].ChatRoomId, actualMessages[i].ChatRoomId);
                Assert.Equal(expectedRooms[i].CreatedUserName, actualMessages[i].CreatedUserName);
                Assert.Equal(expectedRooms[i].Text, actualMessages[i].Text);
            }
        }

        [Fact]
        public void AddAsync_ValidMessage_CallsRepositoryAdd()
        {
            // Arrange
            var messageToAdd = new ChatMessage(Guid.NewGuid(), Guid.NewGuid(), "Test User 1", "Test message 1");
            var messageToAddInput = CreateMessageInput.Create(messageToAdd.CreatedUserId, messageToAdd.ChatRoomId, messageToAdd.CreatedUserName, messageToAdd.Text);

            // Act
            _chatMessageService.CreateMessageAsync(messageToAddInput, CancellationToken.None);

            // Assert
            _chatMessageRepository.Received(1).Add(Arg.Is<ChatMessage>(r =>
                r.CreatedUserId == messageToAdd.CreatedUserId
                && r.ChatRoomId == messageToAdd.ChatRoomId
                && r.CreatedUserName == messageToAdd.CreatedUserName
                && r.Text == messageToAdd.Text
                ));
        }

        [Fact]
        public async Task Can_Create_Message_Successfully()
        {
            // Arrange
            var messageToAddInput = CreateMessageInput.Create(Guid.NewGuid(), Guid.NewGuid(), "Test User 1", "Test message 1");

            // Act
            await _chatMessageService.CreateMessageAsync(messageToAddInput, CancellationToken.None);

            // Assert
            _chatMessageRepository.Received().Add(Arg.Any<ChatMessage>());
            await _chatMessageRepository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public void ShouldReturnCompletedTaskWhenCallSubscribeAsync()
        {
            // arrange
            var task = Task.CompletedTask;
            _bus
                .SubscribeAsync(Arg.Any<Func<Stock, Task>>(), Arg.Any<string>())
                .Returns(task);

            // act
            var result = _chatMessageService.SubscribeAsync(Arg.Any<Func<ChatMessage, Task>>());

            // assert
            result.Should().BeEquivalentTo(task);
        }

        [Fact]
        public void ShouldReturnCompletedTaskWhenCallEnqueueStockToSearchAsync()
        {
            // arrange
            _bus
                .PublishAsync(Arg.Any<ChatMessage>(), Arg.Any<string>())
                .Returns(Task.CompletedTask);
            var message = new ChatMessage(Guid.NewGuid(), Guid.NewGuid(), "Test User 1", "Test message 1");

            // act
            var result = _chatMessageService.EnqueueMessageToInsertAsync(message, CancellationToken.None);

            // assert
            result.Should().BeEquivalentTo(Task.CompletedTask);
        }
    }
}
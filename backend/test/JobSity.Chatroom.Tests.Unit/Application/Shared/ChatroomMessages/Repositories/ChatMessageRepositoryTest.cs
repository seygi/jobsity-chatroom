using JobSity.Chatroom.Application.Shared.ChatroomMessages.Entities;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories;
using JobSity.Chatroom.Application.Shared.Data.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.ChatroomMessages.Repositories
{
    public class ChatMessageRepositoryTest
    {
        private readonly ChatMessageRepository _chatMessageRepository;

        public ChatMessageRepositoryTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ChatMessageDatabase")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            var context = new DataContext(options);
            _chatMessageRepository = new ChatMessageRepository(context);
        }

        [Fact]
        public async Task AddChatMessageAsync_ShouldAdd_NewChatMessage_ToTheDatabase()
        {
            // Arrange
            var chatMessage = new ChatMessage(Guid.NewGuid(), Guid.NewGuid(), "Test User", "Test message");

            // Act
            _chatMessageRepository.Add(chatMessage);
            await _chatMessageRepository.SaveChangesAsync();

            // Assert
            var allChatMessages = await _chatMessageRepository.GetTop50ByChatRoomId(chatMessage.ChatRoomId);
            Assert.Single(allChatMessages);
            var addedChatMessage = allChatMessages.First();
            Assert.Equal(chatMessage.CreatedUserId, addedChatMessage.CreatedUserId);
            Assert.Equal(chatMessage.ChatRoomId, addedChatMessage.ChatRoomId);
            Assert.Equal(chatMessage.CreatedUserName, addedChatMessage.CreatedUserName);
            Assert.Equal(chatMessage.Text, addedChatMessage.Text);
        }

        [Fact]
        public async Task GetChatMessageAsync_ShouldReturn_ChatMessage_WithGivenId()
        {
            // Arrange
            var chatMessage = new ChatMessage(Guid.NewGuid(), Guid.NewGuid(), "Test User", "Test message");

            _chatMessageRepository.Add(chatMessage);
            await _chatMessageRepository.SaveChangesAsync();
            var chatRoomId = chatMessage.Id;

            // Act
            var list = await _chatMessageRepository.GetTop50ByChatRoomId(chatMessage.ChatRoomId);

            var result = list.FirstOrDefault(x => x.Id.Equals(chatRoomId));

            // Assert
            Assert.NotNull(result);

            Assert.Equal(chatMessage.Id, result.Id);
            Assert.Equal(chatMessage.CreatedUserId, result.CreatedUserId);
            Assert.Equal(chatMessage.ChatRoomId, result.ChatRoomId);
            Assert.Equal(chatMessage.CreatedUserName, result.CreatedUserName);
            Assert.Equal(chatMessage.Text, result.Text);
        }

        [Fact]
        public async Task GetChatMessagesAsync_ShouldReturn_AllChatMessages_InTheDatabase()
        {
            // Arrange
            var chatMessage1 = new ChatMessage(Guid.NewGuid(), Guid.NewGuid(), "Test User 1", "Test message 1");
            var chatMessage2 = new ChatMessage(Guid.NewGuid(), chatMessage1.ChatRoomId, "Test User 2", "Test message 2");

            _chatMessageRepository.Add(chatMessage1);
            _chatMessageRepository.Add(chatMessage2);
            await _chatMessageRepository.SaveChangesAsync();

            // Act
            var allChatMessages = await _chatMessageRepository.GetTop50ByChatRoomId(chatMessage1.ChatRoomId);

            // Assert
            Assert.NotNull(allChatMessages);
            Assert.Equal(2, allChatMessages.Count());
        }
    }

}
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories;
using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using JobSity.Chatroom.Application.Shared.Data.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Chatrooms.Repositories
{
    public class ChatRoomRepositoryTest
    {
        private readonly ChatRoomRepository _chatRoomRepository;

        public ChatRoomRepositoryTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ChatRoomDatabase")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            var context = new DataContext(options);
            _chatRoomRepository = new ChatRoomRepository(context);
        }

        [Fact]
        public async Task AddChatRoomAsync_ShouldAdd_NewChatRoom_ToTheDatabase()
        {
            // Arrange
            var chatRoom = new ChatRoom("Test Room", Guid.NewGuid());

            // Act
            _chatRoomRepository.Add(chatRoom);
            await _chatRoomRepository.SaveChangesAsync();

            // Assert
            var allChatRooms = await _chatRoomRepository.GetAllAsync();
            Assert.Single(allChatRooms);
            var addedChatRoom = allChatRooms.First();
            Assert.Equal(chatRoom.Name, addedChatRoom.Name);
            Assert.Equal(chatRoom.CreatedUserId, addedChatRoom.CreatedUserId);
        }

        [Fact]
        public async Task GetChatRoomAsync_ShouldReturn_ChatRoom_WithGivenId()
        {
            // Arrange
            var chatRoom = new ChatRoom("Test Room", Guid.NewGuid());

            _chatRoomRepository.Add(chatRoom);
            await _chatRoomRepository.SaveChangesAsync();
            var chatRoomId = chatRoom.Id;

            // Act
            var list = await _chatRoomRepository.GetAllAsync();

            var result = list.FirstOrDefault(x => x.Id.Equals(chatRoomId));

            // Assert
            Assert.NotNull(result);
            Assert.Equal(chatRoom.Id, result.Id);
            Assert.Equal(chatRoom.Name, result.Name);
            Assert.Equal(chatRoom.CreatedUserId, result.CreatedUserId);
        }

        [Fact]
        public async Task GetChatRoomsAsync_ShouldReturn_AllChatRooms_InTheDatabase()
        {
            // Arrange
            var chatRoom1 = new ChatRoom("Test Room 1", Guid.NewGuid());
            var chatRoom2 = new ChatRoom("Test Room 2", Guid.NewGuid());

            _chatRoomRepository.Add(chatRoom1);
            _chatRoomRepository.Add(chatRoom2);
            await _chatRoomRepository.SaveChangesAsync();

            // Act
            var allChatRooms = await _chatRoomRepository.GetAllAsync();

            // Assert
            Assert.NotNull(allChatRooms);
            Assert.Equal(2, allChatRooms.Count());
        }
    }
}
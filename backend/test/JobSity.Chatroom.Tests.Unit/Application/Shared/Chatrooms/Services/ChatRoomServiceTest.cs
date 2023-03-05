using JobSity.Chatroom.Application.Features.Chatrooms.CreateRoom.UseCase;
using JobSity.Chatroom.Application.Shared.ChatroomMessages.Repositories;
using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using JobSity.Chatroom.Application.Shared.Chatrooms.Services;
using NSubstitute;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Chatrooms.Services
{
    public class ChatRoomServiceTest
    {
        public ChatRoomServiceTest()
        {
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllRooms()
        {
            // Arrange
            var expectedRooms = new List<ChatRoom>
            {
                new ChatRoom("Test Room 1", Guid.NewGuid()),
                new ChatRoom("Test Room 2", Guid.NewGuid()),
                new ChatRoom("Test Room 3", Guid.NewGuid())
            };
            var mockRepository = Substitute.For<IChatRoomRepository>();
            mockRepository.GetAllAsync().Returns(expectedRooms);
            var service = new ChatRoomService(mockRepository);

            // Act
            var actualRooms = (await service.GetAllAsync(CancellationToken.None)).ToArray();

            // Assert
            Assert.Equal(expectedRooms.Count, actualRooms.Length);
            for (int i = 0; i < expectedRooms.Count; i++)
            {
                Assert.Equal(expectedRooms[i].Id, actualRooms[i].Id);
                Assert.Equal(expectedRooms[i].Name, actualRooms[i].Name);
            }
        }

        [Fact]
        public void AddAsync_ValidRoom_CallsRepositoryAdd()
        {
            // Arrange
            var roomToAdd = new ChatRoom("Test Room 3", Guid.Empty);
            var roomToAddInput = CreateRoomInput.Create(roomToAdd.Name);
            var mockRepository = Substitute.For<IChatRoomRepository>();
            var service = new ChatRoomService(mockRepository);

            // Act
            service.CreateChatRoomAsync(roomToAddInput, CancellationToken.None);

            // Assert
            mockRepository.Received(1).Add(Arg.Is<ChatRoom>(r =>
                r.Name == roomToAdd.Name));
        }

        [Fact]
        public async Task Can_Create_ChatRoom_Successfully()
        {
            // Arrange
            var chatRoomRepository = Substitute.For<IChatRoomRepository>();
            var chatRoomService = new ChatRoomService(chatRoomRepository);
            var roomToAddInput = CreateRoomInput.Create("Test Chat Room");

            // Act
            await chatRoomService.CreateChatRoomAsync(roomToAddInput, CancellationToken.None);

            // Assert
            chatRoomRepository.Received().Add(Arg.Any<ChatRoom>());
            await chatRoomRepository.Received(1).SaveChangesAsync();
        }
    }
}
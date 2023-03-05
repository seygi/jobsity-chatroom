using JobSity.Chatroom.Application.Shared.Chatrooms.Entities;
using JobSity.Chatroom.Application.Shared.Data.Postgres;
using Microsoft.EntityFrameworkCore;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Data.Postgres
{
    public class DataContextTests : IDisposable
    {
        private readonly DataContext _dataContext;

        public DataContextTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _dataContext = new DataContext(options);
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        [Fact]
        public void DataContext_Constructor_CreatesInstance()
        {
            Assert.NotNull(_dataContext);
        }

        [Fact]
        public void DataContext_Add_AddsEntity()
        {
            // Arrange
            var entity = new ChatRoom("test", Guid.NewGuid());

            // Act
            _dataContext.Add(entity);
            _dataContext.SaveChanges();

            // Assert
            var result = _dataContext.Set<ChatRoom>().FirstOrDefault(e => e.Id == entity.Id);
            Assert.NotNull(result);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public void DataContext_Find_FindsEntity()
        {
            // Arrange
            var entity = new ChatRoom("test", Guid.NewGuid());
            _dataContext.Add(entity);
            _dataContext.SaveChanges();

            // Act
            var result = _dataContext.Find<ChatRoom>(entity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public void DataContext_AddRange_AddsMultipleEntities()
        {
            // Arrange
            var entities = new List<ChatRoom>
            {
                new ChatRoom("test 1", Guid.NewGuid()),
                new ChatRoom("test 2", Guid.NewGuid()),
                new ChatRoom("test 3", Guid.NewGuid())
            };

            // Act
            _dataContext.AddRange(entities);
            _dataContext.SaveChanges();

            // Assert
            var results = _dataContext.Set<ChatRoom>().ToList();
            Assert.Equal(entities.Count, results.Count);
            foreach (var entity in entities)
            {
                var result = results.FirstOrDefault(e => e.Id == entity.Id);
                Assert.NotNull(result);
                Assert.Equal(entity.Name, result.Name);
            }
        }
    }
}
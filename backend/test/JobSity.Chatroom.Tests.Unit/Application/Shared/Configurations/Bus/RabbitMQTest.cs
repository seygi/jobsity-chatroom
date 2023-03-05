using JobSity.Chatroom.Application.Shared.Configurations.DataBase;
using Rabbit = JobSity.Chatroom.Application.Shared.Configurations.Bus;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Configurations.Bus
{
    public class RabbitMQTest
    {
        private readonly Rabbit.RabbitMQ _rabbit;

        public RabbitMQTest()
        {
            _rabbit = new Rabbit.RabbitMQ
            {
                Host = "127.0.0.1",
                User = "UnitTest",
                Password = "123321",
            };
        }

        [Fact(DisplayName = "ShouldCreateObject")]
        public void ShouldCreateObject()
        {
            // arrange - act - assert
            AssertProperties(_rabbit, "127.0.0.1", "UnitTest", "123321");
        }

        [Fact(DisplayName = "ShouldCreateEmptyObject")]
        public void ShouldCreateEmptyObject()
        {
            // arrange

            // act
            var result = new Rabbit.RabbitMQ();

            // assert
            AssertProperties(result, default, default, default);
        }

        [Fact(DisplayName = "ShouldGetConnectionString")]
        public void ShouldGetConnectionString()
        {
            // arrange
            var connectionStringExpected = "host=127.0.0.1;username=UnitTest;password=123321";

            // act
            var connectionString = _rabbit.GetConnectionString();

            // assert
            connectionString.Should().BeEquivalentTo(connectionStringExpected);
        }

        [Fact(DisplayName = "ShouldValidateProperties")]
        public void ShouldValidateProperties()
        {
            // arrange
            var expectedProperties = new List<AssertProperty>
            {
                new() {Name = "Host", Type = typeof(string)},
                new() {Name = "User", Type = typeof(string)},
                new() {Name = "Password", Type = typeof(string)},
            };

            // act - assert
            typeof(Rabbit.RabbitMQ).ValidateProperties(expectedProperties);
        }

        private static void AssertProperties(Rabbit.RabbitMQ rabbit, string host, string user, string password)
        {
            rabbit.Host.Should().BeEquivalentTo(host);
            rabbit.User.Should().BeEquivalentTo(user);
            rabbit.Password.Should().BeEquivalentTo(password);
        }
    }

}

using JobSity.Chatroom.Application.Shared.Configurations;
using JobSity.Chatroom.Application.Shared.Configurations.DataBase;
using Rabbit = JobSity.Chatroom.Application.Shared.Configurations.Bus;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Configurations
{
    public class ConnectionStringsTest
    {
        [Fact(DisplayName = "ShouldCreateObject")]
        public void ShouldCreateObject()
        {
            // arrange
            var postgres = new Postgres();
            var rabbitMQ = new Rabbit.RabbitMQ();

            // act
            var result = new ConnectionStrings { Postgres = postgres, RabbitMQ = rabbitMQ};

            // assert
            AssertProperties(result, postgres, rabbitMQ);
        }

        [Fact(DisplayName = "ShouldValidateProperties")]
        public void ShouldValidateProperties()
        {
            // arrange
            var expectedProperties = new List<AssertProperty>
            {
                new() {Name = "Postgres", Type = typeof(Postgres)},
                new() {Name = "RabbitMQ", Type = typeof(Rabbit.RabbitMQ)}
            };

            // act - assert
            typeof(ConnectionStrings).ValidateProperties(expectedProperties);
        }

        private static void AssertProperties(ConnectionStrings result, Postgres postgres, Rabbit.RabbitMQ rabbitMQ)
        {
            result.Postgres.Should().Be(postgres);
            result.RabbitMQ.Should().Be(rabbitMQ);
        }
    }
}
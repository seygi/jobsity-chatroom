using JobSity.Chatroom.Application.Shared.Configurations.DataBase;
using JobSity.Chatroom.Tests.Unit;
using Microsoft.AspNetCore.Hosting.Server;

namespace JobSity.Chatroom.Tests.Unit.Application.Shared.Configurations.DataBase
{
    public class PostgresTest
    {
        private readonly Postgres _postgres;

        public PostgresTest()
        {
            _postgres = new Postgres
            {
                Server = "127.0.0.1",
                Port = "1525",
                Database = "UNIT_TEST",
                User = "UnitTest",
                Password = "123321",
            };
        }

        [Fact(DisplayName = "ShouldCreateObject")]
        public void ShouldCreateObject()
        {
            // arrange - act - assert
            AssertProperties(_postgres, "127.0.0.1","1525", "UNIT_TEST", "UnitTest", "123321");
        }

        [Fact(DisplayName = "ShouldCreateEmptyObject")]
        public void ShouldCreateEmptyObject()
        {
            // arrange

            // act
            var result = new Postgres();

            // assert
            AssertProperties(result, default, default, default, default, default);
        }

        [Fact(DisplayName = "ShouldGetConnectionString")]
        public void ShouldGetConnectionString()
        {
            // arrange
            var connectionStringExpected = "Server=127.0.0.1;Database=UNIT_TEST;Port=1525;User Id=UnitTest;Password=123321";

            // act
            var connectionString = _postgres.GetConnectionString();

            // assert
            connectionString.Should().BeEquivalentTo(connectionStringExpected);
        }

        [Fact(DisplayName = "ShouldValidateProperties")]
        public void ShouldValidateProperties()
        {
            // arrange
            var expectedProperties = new List<AssertProperty>
            {
                new() {Name = "Server", Type = typeof(string)},
                new() {Name = "Port", Type = typeof(string)},
                new() {Name = "Database", Type = typeof(string)},
                new() {Name = "User", Type = typeof(string)},
                new() {Name = "Password", Type = typeof(string)},
            };

            // act - assert
            typeof(Postgres).ValidateProperties(expectedProperties);
        }

        private static void AssertProperties(Postgres sqlServer, string server, string port, string database, string user, string password)
        {
            sqlServer.Server.Should().BeEquivalentTo(server);
            sqlServer.Port.Should().BeEquivalentTo(port);
            sqlServer.Database.Should().BeEquivalentTo(database);
            sqlServer.User.Should().BeEquivalentTo(user);
            sqlServer.Password.Should().BeEquivalentTo(password);
        }
    }
}
namespace JobSity.Chatroom.Application.Shared.Configurations
{
    public sealed class ConnectionStrings
    {
        public DataBase.Postgres Postgres { get; set; }
        public Bus.RabbitMQ RabbitMQ { get; set; }
    }
}
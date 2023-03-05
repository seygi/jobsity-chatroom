namespace JobSity.Chatroom.Application.Shared.Configurations.Bus
{
    public sealed class RabbitMQ
    {
        public string Host { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string GetConnectionString()
        {
            return $"host={Host};username={User};password={Password}";
        }
    }

}

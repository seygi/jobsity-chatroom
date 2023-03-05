namespace JobSity.Chatroom.Application.Shared.Configurations.DataBase
{
    public sealed class Postgres
    {
        public string Server { get; set; }
        public string Port { get; set; }
        public string Database { get; set; }
        public string User { get; set; }
        public string Password { get; set; }

        public string GetConnectionString()
        {
            return $"Server={Server};Database={Database};Port={Port};User Id={User};Password={Password}";
        }
    }

}

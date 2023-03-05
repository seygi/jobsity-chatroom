namespace JobSity.Chatroom.Application.Shared.Messaging
{
    public interface IBus
    {
        Task PublishAsync<T>(T message, string queueName);
        Task SubscribeAsync<T>(Func<T, Task> handler, string queueName) where T : class;
    }
}

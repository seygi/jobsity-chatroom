using System.Net;

namespace JobSity.Chatroom.Application.Shared.Notifications
{
    public interface INotificationContext
    {
        NotificationErrors Notifications { get; }
        HttpStatusCode HttpStatusCode { get; }
        bool HasNotifications { get; }

        void Create(HttpStatusCode httpStatusCode, NotificationErrors notificationErrors);
        void Create(HttpStatusCode httpStatusCode, string notificationErrors);
        void Create(HttpStatusCode httpStatusCode);
    }
}

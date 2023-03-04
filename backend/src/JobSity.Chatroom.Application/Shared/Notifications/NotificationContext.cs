using System.Net;

namespace JobSity.Chatroom.Application.Shared.Notifications
{
    public class NotificationContext : INotificationContext
    {
        private NotificationErrors _notifications;
        private HttpStatusCode _httpStatusCode;

        public bool HasNotifications => _notifications.errors.Any();
        public NotificationErrors Notifications => _notifications;
        public HttpStatusCode HttpStatusCode => _httpStatusCode;

        public NotificationContext()
        {
            _notifications = NotificationErrors.Empty;
        }

        public void Create(HttpStatusCode httpStatusCode, NotificationErrors notificationErrors)
        {
            _httpStatusCode = httpStatusCode;
            _notifications = notificationErrors;
        }

        public void Create(HttpStatusCode httpStatusCode, string notificationErrors)
        {
            _httpStatusCode = httpStatusCode;
            _notifications.Add(string.Empty, notificationErrors);
        }

        public void Create(HttpStatusCode httpStatusCode)
        {
            _httpStatusCode = httpStatusCode;
        }
    }
}

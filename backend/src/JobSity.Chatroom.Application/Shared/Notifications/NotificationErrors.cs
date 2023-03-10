using System.Diagnostics.CodeAnalysis;

namespace JobSity.Chatroom.Application.Shared.Notifications
{
    [ExcludeFromCodeCoverage]
    public sealed class NotificationErrors
    {
        private readonly IDictionary<string, IList<string>> _errorMessages = new Dictionary<string, IList<string>>();

        public IDictionary<string, string[]> errors => _errorMessages.ToDictionary(item => item.Key, item => item.Value.ToArray());

        public void Add(string key, string message)
        {
            if (!_errorMessages.ContainsKey(key))
            {
                _errorMessages[key] = new List<string>();
            }

            _errorMessages[key].Add(message);
        }

        public static NotificationErrors Empty => new();
    }
}
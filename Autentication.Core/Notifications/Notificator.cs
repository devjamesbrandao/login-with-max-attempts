using Autentication.Core.Interfaces;

namespace Autentication.Core.Notifications
{
    public class Notificator : INotificator
    {
        private List<Notification> _notifications;

        public Notificator() => _notifications = new List<Notification>();

        public void Handle(Notification notification) => _notifications.Add(notification);

        public List<Notification> GetNotifications() => _notifications;

        public bool HasNotification() => _notifications.Any();
    }
}
using Autentication.Core.Interfaces;
using Autentication.Core.Notifications;

namespace Autentication.Core.Services
{
    public abstract class BaseService
    {
        private readonly INotificator _notificator;

        protected BaseService(INotificator notificator)
        {
            _notificator = notificator;
        }

        protected void Notify(string message) => _notificator.Handle(new Notification(message));
    }
}
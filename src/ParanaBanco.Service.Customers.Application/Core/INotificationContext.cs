using ParanaBanco.Service.Customers.Domain.Notifications;
using System.ComponentModel.DataAnnotations;

namespace ParanaBanco.Service.Customer.Api.Endpoints.Core
{
    public interface INotificationContext
    {
        bool HasNotifications { get; }
        IReadOnlyCollection<Notification> Notifications { get; }

        void AddNotification(Notification notification);
        void AddNotification(string key, string message);
        void AddNotifications(ICollection<Notification> notifications);
        void AddNotifications(IList<Notification> notifications);
        void AddNotifications(IReadOnlyCollection<Notification> notifications);
    }
}

using ParanaBanco.Service.Customers.Domain.Notifications;

namespace ParanaBanco.Service.Customers.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            _notifications = new List<Notification>();
        }
        private List<Notification> _notifications { get; set; }
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public virtual bool IsValid() => false;
        internal void AddNotification(Notification notification)
        {
            _notifications.Add(notification);
        }
    }
}

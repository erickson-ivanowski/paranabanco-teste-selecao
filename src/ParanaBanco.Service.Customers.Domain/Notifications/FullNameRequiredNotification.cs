namespace ParanaBanco.Service.Customers.Domain.Notifications
{
    public class FullNameRequiredNotification : Notification
    {
        public FullNameRequiredNotification() : base(nameof(FullNameRequiredNotification), "The name is required.")
        {
        }
    }
}

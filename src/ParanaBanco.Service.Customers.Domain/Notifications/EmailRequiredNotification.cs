namespace ParanaBanco.Service.Customers.Domain.Notifications
{
    public class EmailRequiredNotification : Notification
    {
        public EmailRequiredNotification() : base(nameof(EmailRequiredNotification), "Email is required.")
        {
        }
    }
}

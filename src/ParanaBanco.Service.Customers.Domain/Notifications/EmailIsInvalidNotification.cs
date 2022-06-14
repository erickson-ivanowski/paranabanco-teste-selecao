namespace ParanaBanco.Service.Customers.Domain.Notifications
{
    public class EmailInvalidNotification : Notification
    {
        public EmailInvalidNotification() : base(nameof(EmailInvalidNotification), "The email entered is invalid.")
        {
        }
    }
}

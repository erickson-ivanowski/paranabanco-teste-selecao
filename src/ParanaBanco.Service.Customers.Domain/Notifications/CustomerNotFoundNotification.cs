namespace ParanaBanco.Service.Customers.Domain.Notifications
{
    public class CustomerNotFoundNotification : Notification
    {
        public CustomerNotFoundNotification() : base(nameof(CustomerNotFoundNotification), "Customer not found.")
        {
        }
    }
}

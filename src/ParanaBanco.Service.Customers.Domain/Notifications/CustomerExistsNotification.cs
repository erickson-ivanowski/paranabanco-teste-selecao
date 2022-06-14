namespace ParanaBanco.Service.Customers.Domain.Notifications
{
    public class CustomerExistsNotification : Notification
    {
        public CustomerExistsNotification() : base(nameof(CustomerExistsNotification), "The customer already exists.")
        {
        }
    }
}

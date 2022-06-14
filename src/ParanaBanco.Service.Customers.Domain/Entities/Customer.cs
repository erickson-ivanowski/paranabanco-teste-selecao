using ParanaBanco.Service.Customers.Domain.Notifications;
using ParanaBanco.Service.Customers.Domain.Services;
using System.Text.RegularExpressions;

namespace ParanaBanco.Service.Customers.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer(Guid id, string email, string fullName)
        {
            Id = id;
            Email = email;
            FullName = fullName;
        }

        public Customer(string email, string fullName)
        {
            Id = Guid.NewGuid();
            Email = email;
            FullName = fullName;
        }

        public Guid Id { get; }
        public string Email { get; private set; }
        public string FullName { get; private set; }

        private bool EmailUpdated = false;
        private bool NameUpdated = false;

        public Customer SetEmail(string email)
        {
            EmailUpdated = true;
            Email = email;
            return this;
        }

        public Customer SetFullName(string fullName)
        {
            NameUpdated = true;
            FullName = fullName;
            return this;
        }


        private CustomerServices DomainService { get; set; }
        public void SetDomainService(CustomerServices domainService) => DomainService = domainService;

        public override async Task<bool> IsValidAsync()
        {
            await ValidateEmailAsync();
            ValidateFullName();
            return !Notifications.Any();
        }

        private async Task ValidateEmailAsync()
        {
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

            if (string.IsNullOrEmpty(Email) || string.IsNullOrWhiteSpace(Email))
                AddNotification(new EmailRequiredNotification());
            else if (regex.IsMatch(Email) is false)
                AddNotification(new EmailInvalidNotification());
            else if (EmailUpdated is false && await DomainService.EmailExists(Email))
                AddNotification(new CustomerExistsNotification());
            else if (NameUpdated is false && await DomainService.EmailExists(Email))
                AddNotification(new CustomerExistsNotification());
        }

        private void ValidateFullName()
        {
            var regex = new Regex("^[a-zA-Z]+ [a-zA-Z ]+[a-z]$");

            if (string.IsNullOrEmpty(FullName) || string.IsNullOrWhiteSpace(FullName) || regex.IsMatch(FullName) is false)
                AddNotification(new FullNameRequiredNotification());
        }
    }
}

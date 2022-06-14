using ParanaBanco.Service.Customers.Domain.Entities;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;
using ParanaBanco.Service.Customers.Domain.Interfaces.Services;
using ParanaBanco.Service.Customers.Domain.Notifications;

namespace ParanaBanco.Service.Customers.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> CustomerExists(Customer customer)
        {
            if (await _customerRepository.GetCustomerAsync(customer.Email) is var customerSearched && customerSearched is null)
                return false;

            if (customer.Id == customerSearched.Id) // Is update of existent customer
                return false;

            customer.AddNotification(new CustomerExistsNotification());

            return true;
        }

    }
}

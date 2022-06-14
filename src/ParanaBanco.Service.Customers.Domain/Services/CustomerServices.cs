using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;

namespace ParanaBanco.Service.Customers.Domain.Services
{
    public class CustomerServices
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<bool> EmailExists(string? email)
        {
            if (await _customerRepository.GetCustomerAsync(email) is null)
                return false;

            return true;
        }

    }
}

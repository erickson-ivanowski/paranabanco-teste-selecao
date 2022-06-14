using ParanaBanco.Service.Customers.Domain.Entities;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;
using ParanaBanco.Service.Customers.Domain.Interfaces.Services;
using ParanaBanco.Service.Customers.Domain.Services;

namespace ParanaBanco.Service.Customers.Domain.Tests.DomainServices
{
    public class CustomerMock
    {
        private readonly Mock<ICustomerRepository> CustomerRepositoryMock;
        private readonly CustomerService CustomerService;

        public CustomerMock()
        {
            var _mocker = new AutoMocker();

            CustomerRepositoryMock = _mocker.GetMock<ICustomerRepository>();
            CustomerService = _mocker.CreateInstance<CustomerService>();
        }

        public void SetupRepository(string email, Customer customer)
        {
            CustomerRepositoryMock.Setup(x => x.GetCustomerAsync(email)).ReturnsAsync(customer);
        }

        public CustomerService GetCustomerService()
        {
            return CustomerService;
        }
    }
}

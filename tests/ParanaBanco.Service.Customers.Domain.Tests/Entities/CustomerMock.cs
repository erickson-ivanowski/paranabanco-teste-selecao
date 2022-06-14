using ParanaBanco.Service.Customers.Domain.Entities;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;
using ParanaBanco.Service.Customers.Domain.Services;

namespace ParanaBanco.Service.Customers.Domain.Tests.Entities
{
    public class CustomerMock
    {
        private readonly Mock<ICustomerRepository> CustomerRepositoryMock;
        private readonly CustomerServices CustomerServices;

        public CustomerMock()
        {
            var _mocker = new AutoMocker();
            
            CustomerRepositoryMock = _mocker.GetMock<ICustomerRepository>();

            CustomerServices = _mocker.CreateInstance<CustomerServices>();
        }

        public void SetupRepository(string email, Customer customer)
        {
            CustomerRepositoryMock.Setup(x => x.GetCustomerAsync(email)).ReturnsAsync(customer);
        }

        public CustomerServices GetCustomerServices()
        {
            return CustomerServices;
        }
    }
}

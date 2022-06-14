using ParanaBanco.Service.Customers.Application.Core;
using ParanaBanco.Service.Customers.Domain.Entities;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;
using ParanaBanco.Service.Customers.Domain.Interfaces.Services;

namespace ParanaBanco.Service.Customers.Application.Tests.Mocks
{
    public class CustomerMock<TCustomerClass> where TCustomerClass : class
    {
        public readonly Mock<ICustomerRepository> CustomerRepositoryMock;
        public readonly Mock<ICustomerService> CustomerServiceMock;
        public readonly Mock<INotificationContext> NotificationContextMock;
        private readonly TCustomerClass CustomerClassMock;

        public CustomerMock()
        {
            var _mocker = new AutoMocker();

            CustomerRepositoryMock = _mocker.GetMock<ICustomerRepository>();
            CustomerServiceMock = _mocker.GetMock<ICustomerService>();
            NotificationContextMock = _mocker.GetMock<INotificationContext>();
            CustomerClassMock = _mocker.CreateInstance<TCustomerClass>();
        }

        public void SetupGetCustomerByEmail(string email, Customer customer)
        {
            CustomerRepositoryMock.Setup(x => x.GetCustomerAsync(email))
                .ReturnsAsync(customer);
        }

        public void SetupGetAllCustomers(IEnumerable<Customer> customers)
        {
            CustomerRepositoryMock.Setup(x => x.GetAllAsync())
                .ReturnsAsync(customers);
        }

        public void SetupCustomerExistsCustomerServiceMock(Customer customer, bool exists)
        {
            CustomerServiceMock.Setup(x => x.CustomerExists(customer))
                .ReturnsAsync(exists);
        }

        public TCustomerClass GetInstance()
        {
            return CustomerClassMock;
        }
    }
}

using ParanaBanco.Service.Customers.Application.Core;
using ParanaBanco.Service.Customers.Domain.Entities;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;

namespace ParanaBanco.Service.Customers.Application.Tests.Mocks
{
    public class CustomerMock<TCustomerClass> where TCustomerClass : class
    {
        public readonly Mock<ICustomerRepository> CustomerRepositoryMock;
        public readonly Mock<INotificationContext> NotificationContextMock;
        private readonly TCustomerClass CustomerClassMock;

        public CustomerMock()
        {
            var _mocker = new AutoMocker();

            CustomerRepositoryMock = _mocker.GetMock<ICustomerRepository>();
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

        public TCustomerClass GetInstance()
        {
            return CustomerClassMock;
        }
    }
}

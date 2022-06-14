using ParanaBanco.Service.Customers.Application.Commands;
using ParanaBanco.Service.Customers.Application.Query;
using ParanaBanco.Service.Customers.Application.QueryHandlers;
using ParanaBanco.Service.Customers.Application.Tests.Mocks;
using ParanaBanco.Service.Customers.Application.ViewModels;
using ParanaBanco.Service.Customers.Domain.Entities;

namespace ParanaBanco.Service.Customers.Application.Tests.QueryHandlers
{
    public class GetCustomersQueryHandlerTests
    {
        private readonly CustomerMock<GetCustomersQueryHandler> CustomerMock;

        public GetCustomersQueryHandlerTests()
        {
            CustomerMock = new CustomerMock<GetCustomersQueryHandler>();
        }

        [Fact]
        public async Task Given_Customers_NotFound_Should_Return_Empty_List()
        {
            // Arrange
            var customers = Enumerable.Empty<Customer>();
            CustomerMock.SetupGetAllCustomers(customers);

            var query = new GetCustomersQuery();
            // Act
            var result = await CustomerMock.GetInstance().Handle(query, CancellationToken.None);

            // Assert
            CustomerMock.CustomerRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
            result.Count().Should().Be(0);
        }

        [Fact]
        public async Task Given_Customers_Should_Return_In_List()
        {
            // Arrange
            var customers = new List<Customer>()
            {
                new Customer("teste@email.com","Fulano de Tal"),
                new Customer("teste2@email.com","Fulanos de Tal"),
                new Customer("teste3@email.com","Fulanoss de Tal")
            };

            CustomerMock.SetupGetAllCustomers(customers);

            var query = new GetCustomersQuery();
            // Act
            var result = await CustomerMock.GetInstance().Handle(query, CancellationToken.None);

            // Assert
            CustomerMock.CustomerRepositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
            result.Count().Should().Be(3);
        }
    }
}

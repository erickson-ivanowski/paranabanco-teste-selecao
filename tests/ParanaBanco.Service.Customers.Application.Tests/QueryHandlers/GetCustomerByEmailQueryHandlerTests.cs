using ParanaBanco.Service.Customers.Application.Query;
using ParanaBanco.Service.Customers.Application.QueryHandlers;
using ParanaBanco.Service.Customers.Application.Tests.Mocks;
using ParanaBanco.Service.Customers.Domain.Entities;

namespace ParanaBanco.Service.Customers.Application.Tests.QueryHandlers
{
    public class GetCustomerByEmailQueryHandlerTests
    {
        private readonly CustomerMock<GetCustomerByEmailQueryHandler> CustomerMock;

        public GetCustomerByEmailQueryHandlerTests()
        {
            CustomerMock = new CustomerMock<GetCustomerByEmailQueryHandler>();
        }

        [Fact]
        public async Task Given_Customer_NotFound_Should_Return_Null()
        {
            // Arrange
            var query = new GetCustomerByEmailQuery
            {
                Email = "cwberick@live.nl"
            };

            CustomerMock.SetupGetCustomerByEmail(It.IsAny<string>(), null);

            // Act
            var result = await CustomerMock.GetInstance().Handle(query, CancellationToken.None);

            // Assert
            CustomerMock.CustomerRepositoryMock.Verify(x => x.GetCustomerAsync(query.Email), Times.Once);
            result.Should().BeNull();
        }

        [Fact]
        public async Task Given_Customer_Found_Should_Return()
        {
            // Arrange
            var name = "Erickson Ivanowski";
            var email = "cwberick@live.nl";

            var query = new GetCustomerByEmailQuery
            {
                Email = email
            };

            var customer = new Customer(email, name);

            CustomerMock.SetupGetCustomerByEmail(email,customer);

            // Act
            var result = await CustomerMock.GetInstance().Handle(query, CancellationToken.None);

            // Assert
            CustomerMock.CustomerRepositoryMock.Verify(x => x.GetCustomerAsync(query.Email), Times.Once);
            result.Email.Should().Be(email);
            result.FullName.Should().Be(name);
        }
    }
}

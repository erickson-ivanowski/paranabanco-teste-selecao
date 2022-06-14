using ParanaBanco.Service.Customers.Application.CommandHandlers;
using ParanaBanco.Service.Customers.Application.Commands;
using ParanaBanco.Service.Customers.Application.Tests.Mocks;
using ParanaBanco.Service.Customers.Domain.Entities;

namespace ParanaBanco.Service.Customers.Application.Tests.CommandHandlers
{
    public class DeleteCustomerCommandHandlerTests
    {
        private readonly CustomerMock<DeleteCustomerCommandHandler> CustomerMock;

        public DeleteCustomerCommandHandlerTests()
        {
            CustomerMock = new CustomerMock<DeleteCustomerCommandHandler>();
        }

        [Fact]
        public async Task Given_Customer_NotFound_Should_Add_Notification()
        {
            // Arrange
            CustomerMock.SetupGetCustomerByEmail(It.IsAny<string>(), null);

            var command = new DeleteCustomerCommand
            {
                Email = "cwberick@live.com"
            };

            // Act
            await CustomerMock.GetInstance().Handle(command, CancellationToken.None);

            // Assert
            CustomerMock.CustomerRepositoryMock.Verify(x => x.GetCustomerAsync(command.Email), Times.Once);
            CustomerMock.CustomerRepositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Fact]
        public async Task Given_Valid_Customer_Should_Add_Notification()
        {
            // Arrange
            var email = "cwberick@live.com";
            var name = "Erickson Ivanowski";
            var customer = new Customer(email, name);
            var command = new DeleteCustomerCommand
            {
                Email = email
            };

            CustomerMock.SetupGetCustomerByEmail(email, customer);

            // Act
            await CustomerMock.GetInstance().Handle(command, CancellationToken.None);

            // Assert
            CustomerMock.CustomerRepositoryMock.Verify(x => x.GetCustomerAsync(command.Email), Times.Once);
            CustomerMock.CustomerRepositoryMock.Verify(x => x.DeleteAsync(It.Is<Customer>(x => x.Email == email)), Times.Once);
            CustomerMock.CustomerRepositoryMock.Verify(x => x.DeleteAsync(It.Is<Customer>(x => x.FullName == name)), Times.Once);
        }
    }
}

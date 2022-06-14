using ParanaBanco.Service.Customers.Application.CommandHandlers;
using ParanaBanco.Service.Customers.Application.Commands;
using ParanaBanco.Service.Customers.Application.Tests.Mocks;
using ParanaBanco.Service.Customers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Service.Customers.Application.Tests.CommandHandlers
{
    public class UpdateCustomerCommandHandlerTests
    {
        private readonly CustomerMock<UpdateCustomerCommandHandler> CustomerMock;

        public UpdateCustomerCommandHandlerTests()
        {
            CustomerMock = new CustomerMock<UpdateCustomerCommandHandler>();
        }

        [Fact]
        public async Task Given_NotFound_Customer_Should_Add_Notification()
        {
            // Arrange
            CustomerMock.SetupGetCustomerByEmail(It.IsAny<string>(), null);

            var command = new UpdateCustomerCommand
            {
                Email = "cwberick@live.com",
                NewFullName = "Erickson Ivanowski",
                NewEmail = "erickson@blabla.com"
            };

            // Act
            await CustomerMock.GetInstance().Handle(command, CancellationToken.None);

            // Assert
            CustomerMock.CustomerRepositoryMock.Verify(x => x.GetCustomerAsync(command.Email), Times.Once);
            CustomerMock.CustomerRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Fact]
        public async Task Given_Invalid_Customer_Should_Add_Notification()
        {
            // Arrange
            var command = new UpdateCustomerCommand
            {
                Email = "cwberick@live.com",
                NewFullName = "Erickson",
                NewEmail = "erickson@com"
            };
            var customer = new Customer(command.Email, "Erickson Ivanowski");
            CustomerMock.SetupGetCustomerByEmail(command.Email, customer);

            // Act
            await CustomerMock.GetInstance().Handle(command, CancellationToken.None);

            // Assert
            CustomerMock.CustomerRepositoryMock.Verify(x => x.GetCustomerAsync(command.Email), Times.Once);
            CustomerMock.CustomerRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Fact]
        public async Task Given_Valid_Customer_Should_Update_Customer()
        {
            // Arrange
            var command = new UpdateCustomerCommand
            {
                Email = "cwberick@live.com",
                NewFullName = "Erickson Bet Ivanowski",
                NewEmail = "erickson@live.com"
            };
            var customer = new Customer(command.Email, "Erickson Ivanowski");
            CustomerMock.SetupGetCustomerByEmail(command.Email, customer);

            // Act
            await CustomerMock.GetInstance().Handle(command, CancellationToken.None);

            // Assert
            CustomerMock.CustomerRepositoryMock.Verify(x => x.GetCustomerAsync(command.Email), Times.Once);
            CustomerMock.CustomerRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Customer>(x => x.Email == command.NewEmail)), Times.Once);
            CustomerMock.CustomerRepositoryMock.Verify(x => x.UpdateAsync(It.Is<Customer>(x => x.FullName == command.NewFullName)), Times.Once);
        }
    }
}

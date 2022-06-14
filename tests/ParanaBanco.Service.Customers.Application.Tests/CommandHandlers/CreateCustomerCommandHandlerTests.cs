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
    public class CreateCustomerCommandHandlerTests
    {
        private readonly CustomerMock<CreateCustomerCommandHandler> CustomerMock;

        public CreateCustomerCommandHandlerTests()
        {
            CustomerMock = new CustomerMock<CreateCustomerCommandHandler>();
        }

        [Fact]
        public async Task Given_Invalid_Customer_Should_Add_Notification()
        {
            // Arrange
            CustomerMock.SetupGetCustomerByEmail(It.IsAny<string>(), null);

            var command = new CreateCustomerCommand
            {
                Email = "cwberick@live",
                FullName = "Erickson Ivanowski"
            };

            // Act
            await CustomerMock.GetInstance().Handle(command, CancellationToken.None);

            // Assert
            CustomerMock.CustomerRepositoryMock.Verify(x => x.GetCustomerAsync(command.Email), Times.Never);
            CustomerMock.CustomerRepositoryMock.Verify(x => x.SaveAsync(It.IsAny<Customer>()), Times.Never);
        }

        [Fact]
        public async Task Given_Valid_Customer_Should_Add_Notification()
        {
            // Arrange
            CustomerMock.SetupGetCustomerByEmail(It.IsAny<string>(), null);

            var command = new CreateCustomerCommand
            {
                Email = "cwberick@live.com",
                FullName = "Erickson Ivanowski"
            };

            // Act
            await CustomerMock.GetInstance().Handle(command, CancellationToken.None);

            // Assert
            CustomerMock.CustomerRepositoryMock.Verify(x => x.GetCustomerAsync(command.Email), Times.Once);
            CustomerMock.CustomerRepositoryMock.Verify(x => x.SaveAsync(It.Is<Customer>(x => x.Email == command.Email)), Times.Once);
            CustomerMock.CustomerRepositoryMock.Verify(x => x.SaveAsync(It.Is<Customer>(x => x.FullName == command.FullName)), Times.Once);
        }
    }
}

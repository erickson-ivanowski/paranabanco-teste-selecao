using FluentAssertions;
using ParanaBanco.Service.Customers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParanaBanco.Service.Customers.Domain.Tests.DomainServices
{
    public class CustomerServiceTest
    {
        [Fact]
        public async Task Given_Customer_NotFound_Should_Return_False()
        {
            // Arrange
            var mock = new CustomerMock();
            mock.SetupRepository(It.IsAny<string>(), null);

            var customer = new Customer("email@teste.com", "Erickson Ivanowski");

            // Act
            var result = await mock.GetCustomerService().CustomerExists(customer);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Given_Same_Customer_Found_Should_Return_False()
        {
            // Arrange
            var mock = new CustomerMock();
            var customer = new Customer("email@teste.com", "Erickson Ivanowski");

            mock.SetupRepository(It.IsAny<string>(), customer);


            // Act
            var result = await mock.GetCustomerService().CustomerExists(customer);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Given_Different_Customer_With_Same_Email_Should_Return_True()
        {
            // Arrange
            var mock = new CustomerMock();
            var customer = new Customer("email@teste.com", "Erickson Ivanowski");
            var customerRepository = new Customer("email@teste.com", "Erickson Ivanowski");

            mock.SetupRepository("email@teste.com", customerRepository);

            // Act
            var result = await mock.GetCustomerService().CustomerExists(customer);

            // Assert
            result.Should().BeTrue();
        }
    }
}

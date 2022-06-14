using FluentAssertions;
using ParanaBanco.Service.Customers.Domain.Entities;
using ParanaBanco.Service.Customers.Infrastructure.Data.Mapping;

namespace ParanaBanco.Service.Customers.Infrastructure.Tests.Mapping
{
    public class CustomerMappingTests
    {
        [Fact]
        public void Given_Customer_Entity_Should_Be_Converted_On_Model()
        {
            // Arrange
            var customer = new Customer("email@teste.com", "Erickson Ivanowski");

            // Act
            var model = customer.AsModel();

            // Assert
            model.Email.Should().Be(customer.Email);
            model.FullName.Should().Be(customer.FullName);
            model.Id.Should().Be(customer.Id);
        }

        [Fact]
        public void Given_Customer_Model_Should_Be_Converted_On_Entity()
        {
            // Arrange
            var customerModel = new Data.Models.Customer()
            {
                Email = "email@teste.com",
                FullName = "Erickson Ivanowski",
                Id = Guid.NewGuid()
            };

            // Act
            var customer = customerModel.AsEntity();

            // Assert
            customer.Email.Should().Be(customerModel.Email);
            customer.FullName.Should().Be(customerModel.FullName);
            customer.Id.Should().Be(customerModel.Id);
        }
    }
}

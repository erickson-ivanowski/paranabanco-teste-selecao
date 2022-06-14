using ParanaBanco.Service.Customers.Application.Mapping;
using ParanaBanco.Service.Customers.Domain.Entities;

namespace ParanaBanco.Service.Customers.Application.Tests.Mapping
{
    public class CustomerMappingTests
    {
        [Fact]
        public void Given_Customer_Should_Return_View_Model()
        {
            // Arrange
            var email = "cwberick@live.nl";
            var name = "Erickson Ivanowski";
            var customer = new Customer(email, name);

            // Act
            var viewModel = customer.AsViewModel();

            // Assert
            viewModel.Email.Should().Be(email);
            viewModel.FullName.Should().Be(name);
        }
    }
}

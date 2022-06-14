using FluentAssertions;
using ParanaBanco.Service.Customers.Domain.Entities;
using ParanaBanco.Service.Customers.Domain.Notifications;

namespace ParanaBanco.Service.Customers.Domain.Tests.Entities
{
    public class CustomerTests
    {
        [Fact]
        public void Test_Constructor_Customer_Should_Be_Return_Correct_Properties()
        {
            // Arrange
            var email = "teste@email.com";
            var name = "Fulano da Silva";

            // Act
            var customer = new Customer(email, name);

            // Assert
            customer.Email.Should().Be(email);
            customer.FullName.Should().Be(name);
        }

        [Fact]
        public void Test_Method_SetEmail_Should_Be_Return_Correct_Propertie()
        {
            // Arrange
            var email = "teste@email.com";
            var name = "Fulano da Silva";
            var newEmail = "testando@email.com";
            var customer = new Customer(email, name);

            // Act
            customer.SetEmail(newEmail);

            // Assert
            customer.Email.Should().Be(newEmail);
        }

        [Fact]
        public void Test_Method_SetFullName_Should_Be_Return_Correct_Propertie()
        {
            // Arrange
            var email = "teste@email.com";
            var name = "Fulano da Silva";
            var newFullName = "Erickson Ivanowski";
            var customer = new Customer(email, name);

            // Act
            customer.SetFullName(newFullName);

            // Assert
            customer.FullName.Should().Be(newFullName);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("             ")]
        public async Task Given_Customer_With_Null_Empty_Whitespace_Email_Should_Add_Notification(string email)
        {
            // Arrange
            var customer = new Customer(email, "Fulano da silva");
            var notification = new EmailRequiredNotification();
            // Act
            await customer.IsValidAsync();

            // Assert
            customer.Notifications.FirstOrDefault().Key.Should().Be(notification.Key);
            customer.Notifications.FirstOrDefault().Message.Should().Be(notification.Message);
        }

        [Theory]
        [InlineData("huhu%shshs.com")]
        [InlineData("shshs @shshs.com")]
        [InlineData("7878,@sisjs.com")]
        [InlineData("err@.com")]
        [InlineData("err@dasdas")]
        public async Task Given_Customer_With_Invalid_Format_Email_Should_Add_Notification(string email)
        {
            // Arrange
            var customer = new Customer(email, "Fulano da silva");
            var notification = new EmailInvalidNotification();

            // Act
            await customer.IsValidAsync();

            // Assert
            customer.Notifications.FirstOrDefault().Key.Should().Be(notification.Key);
            customer.Notifications.FirstOrDefault().Message.Should().Be(notification.Message);
        }

        [Fact]
        public async Task Given_Customer_Existent_Should_Add_Notification()
        {
            // Arrange
            var email = "email@existente.com";
            var customer = new Customer(email, "Fulano da silva");
            var notification = new CustomerExistsNotification();           
            var mock = new CustomerMock();
            mock.SetupRepository(email, customer);
            customer.SetDomainService(mock.GetCustomerServices());

            // Act
            await customer.IsValidAsync();

            // Assert
            customer.Notifications.FirstOrDefault().Key.Should().Be(notification.Key);
            customer.Notifications.FirstOrDefault().Message.Should().Be(notification.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("             ")]
        [InlineData("Erickson")]
        [InlineData("Erickson ")]
        [InlineData(" Erickson Ivanowski")]
        [InlineData("Erickson B. Ivanowski")]
        [InlineData("Erickson Ivanowski ")]
        [InlineData(" Erickson Ivanowski ")]
        public async Task Given_Customer_With_Null_Empty_Whitespace_FullName_Should_Add_Notification(string fullName)
        {
            // Arrange
            var customer = new Customer("email@dominio.com", fullName);
            var notification = new FullNameRequiredNotification();

            var mock = new CustomerMock();
            mock.SetupRepository(It.IsAny<string>(), null);
            customer.SetDomainService(mock.GetCustomerServices());

            // Act
            await customer.IsValidAsync();

            // Assert
            customer.Notifications.FirstOrDefault().Key.Should().Be(notification.Key);
            customer.Notifications.FirstOrDefault().Message.Should().Be(notification.Message);
        }

        [Theory]
        [InlineData("cwberick@live.nl","Erickson Ivanowski")]
        [InlineData("cwberick@live.com", "Erickson B Ivanowski")]
        [InlineData("erick.ivanowski@gmail.com", "Erickson Bet Ivanowski")]
        public async Task Given_Customer_Should_Be_Valid(string email, string fullName)
        {
            // Arrange
            var customer = new Customer(email, fullName);

            var mock = new CustomerMock();
            mock.SetupRepository(It.IsAny<string>(), null);
            customer.SetDomainService(mock.GetCustomerServices());

            // Act
            await customer.IsValidAsync();

            // Assert
            customer.Notifications.Any().Should().BeFalse();
        }
    }
}

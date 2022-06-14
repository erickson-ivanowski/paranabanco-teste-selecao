using ParanaBanco.Service.Customers.Domain.Entities;

namespace ParanaBanco.Service.Customers.Domain.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<bool> CustomerExists(Customer customer);
    }
}
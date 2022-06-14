using ParanaBanco.Service.Customers.Domain.Entities;

namespace ParanaBanco.Service.Customers.Domain.Interfaces.Repositories
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Task<Customer> GetCustomerAsync(string email);
    }
}

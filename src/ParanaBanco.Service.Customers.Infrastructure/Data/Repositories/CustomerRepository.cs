using Microsoft.EntityFrameworkCore;
using ParanaBanco.Service.Customers.Domain.Entities;
using ParanaBanco.Service.Customers.Domain.Interfaces.Repositories;
using ParanaBanco.Service.Customers.Infrastructure.Data.Config;
using ParanaBanco.Service.Customers.Infrastructure.Data.Mapping;

namespace ParanaBanco.Service.Customers.Infrastructure.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {

        private readonly CustomersDbContext _dbContext;

        public CustomerRepository(CustomersDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        async Task<Customer> ICustomerRepository.GetCustomerAsync(string email)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(c => c.Email == email);
            return customer?.AsEntity();
        }

        async Task<IEnumerable<Customer>> IRepository<Customer>.GetAllAsync()
        {
            var customers = await _dbContext.Customers.ToListAsync();
            return customers.Select(c => c.AsEntity());
        }

        public async Task<bool> SaveAsync(Customer entity)
        {
            _dbContext.Customers.Add(entity.AsModel());
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Customer entity)
        {
            _dbContext.Customers.Update(entity.AsModel());
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Customer entity)
        {
            _dbContext.Customers.Remove(entity.AsModel());
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}

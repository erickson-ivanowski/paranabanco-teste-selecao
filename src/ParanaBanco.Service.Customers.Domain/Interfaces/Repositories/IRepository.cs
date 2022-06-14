using ParanaBanco.Service.Customers.Domain.Interfaces;

namespace ParanaBanco.Service.Customers.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entities.Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<bool> SaveAsync(TEntity entity);
        Task<bool> UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
    }
}

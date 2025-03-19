using RetoStore.Entities;

namespace RetoStore.Repositories.Interfaces;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
    Task<Customer?> GetByEmailAsync(string email);
}


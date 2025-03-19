using Microsoft.EntityFrameworkCore;
using RetoStore.Entities;
using RetoStore.Persistence;
using RetoStore.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Repositories.Implementations;

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Customer?> GetByEmailAsync(string email)
    {
        return await context.Set<Customer>().FirstOrDefaultAsync(x => x.Email == email);
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RetoStore.Dto.Request;
using RetoStore.Entities;
using RetoStore.Entities.Info;
using RetoStore.Persistence;
using RetoStore.Repositories.Interfaces;
using RetoStore.Repositories.Utils;
using System.Data;
using System.Linq.Expressions;

namespace RetoStore.Repositories.Implementations;

public class SaleRepository : RepositoryBase<Sale>, ISaleRepository
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public SaleRepository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : base(context)
    {
        this.httpContextAccessor = httpContextAccessor;
    }

    public async Task CreateTransactionAsync()
    {
        await context.Database.BeginTransactionAsync(IsolationLevel.Serializable);
    }

    public async Task RollBackAsync()
    {
        await context.Database.RollbackTransactionAsync();
    }
    public override async Task<int> AddAsync(Sale entity)
    {
        entity.SaleDate = DateTime.Now;
        var nextNumber = await context.Set<Sale>().CountAsync() + 1;
        entity.OperationNumber = $"{nextNumber:000000}";

        //add entity to context
        await context.AddAsync(entity);

        return entity.Id;
    }

    public override async Task UpdateAsync()
    {
        await context.Database.CommitTransactionAsync();
        await base.UpdateAsync();
    }
    public override async Task<Sale?> GetAsync(int id)
    {
        return await context.Set<Sale>()
            .Include(x => x.Customer)
            .Include(x => x.Event)
            .ThenInclude(x => x.Genre)
            .Where(x => x.Id == id)
            .AsNoTracking()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<Sale>> GetAsync<TKey>(Expression<Func<Sale, bool>> predicate,
        Expression<Func<Sale, TKey>> orderBy,
        PaginationDto pagination)
    {
        var queryable = context.Set<Sale>()
            .Include(x => x.Customer)
            .Include(x => x.Event)
            .ThenInclude(x => x.Genre)
            .Where(predicate)
            .OrderBy(orderBy)
            .AsNoTracking()
            .IgnoreQueryFilters()
            .AsQueryable();

        await httpContextAccessor.HttpContext.InsertarPaginacionHeader(queryable);
        var response = await queryable.Paginate(pagination).ToListAsync();
        return response;
    }

    public async Task<ICollection<ReportInfo>> GetSaleReportAsync(DateTime dateStart, DateTime dateEnd)
    {
        var query = context.Database
            .SqlQueryRaw<ReportInfo>(
            @"SELECT c.Title as EventName, SUM(s.Total) as Total
            FROM Apptelink.Sale s
            INNER JOIN Apptelink.Event c ON c.Id = s.EventId
            Where s.SaleDate >= {0} AND s.SaleDate <= {1}
            GROUP BY c.Title",
            dateStart, dateEnd);

        return await query.ToListAsync();
    }
}
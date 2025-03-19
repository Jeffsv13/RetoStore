using RetoStore.Dto.Request;
using RetoStore.Entities;
using RetoStore.Entities.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Repositories.Interfaces;

public interface ISaleRepository : IRepositoryBase<Sale>
{
    Task CreateTransactionAsync();
    Task RollBackAsync();
    Task<ICollection<Sale>> GetAsync<TKey>(Expression<Func<Sale, bool>> predicate,
        Expression<Func<Sale, TKey>> orderBy,
        PaginationDto pagination);

    Task<ICollection<ReportInfo>> GetSaleReportAsync(DateTime dateStart, DateTime dateEnd);
}

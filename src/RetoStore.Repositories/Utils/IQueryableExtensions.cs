using RetoStore.Dto.Request;

namespace RetoStore.Repositories.Utils;

public static class IQueryableExtensions
{
    public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDto paginacionDto)
    {
        return queryable
            .Skip((paginacionDto.Page - 1) * paginacionDto.RecordsPerPage)
            .Take(paginacionDto.RecordsPerPage);
    }
}

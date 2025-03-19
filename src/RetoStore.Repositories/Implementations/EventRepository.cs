using Microsoft.EntityFrameworkCore;
using RetoStore.Entities;
using RetoStore.Persistence;
using RetoStore.Repositories.Interfaces;
using System.Net.Http;
using System;
using RetoStore.Entities.Info;
using Microsoft.AspNetCore.Http;
using RetoStore.Dto.Request;
using RetoStore.Repositories.Utils;

namespace RetoStore.Repositories.Implementations;
public class EventRepository : RepositoryBase<Event>, IEventRepository
{
    private readonly IHttpContextAccessor httpContext;

    public EventRepository(ApplicationDbContext context, IHttpContextAccessor httpContext) : base(context)
    {
        this.httpContext = httpContext;
    }
    public override async Task<ICollection<Event>> GetAsync()
    {
        //eager loading approach
        return await context.Set<Event>()
            .Include(x => x.Genre)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Event?> GetAsyncById(int id)
    {
        return await context.Set<Event>()
            .Include(x => x.Genre)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }
    public async Task<ICollection<EventInfo>> GetAsync(string? title, PaginationDto pagination)
    {
        //lazy loading approach
        var queryable = context.Set<Event>()
            .Where(x => x.Title.Contains(title ?? string.Empty))
            .IgnoreQueryFilters()
            .AsNoTracking()
            .Select(x => new EventInfo
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ExtendedDescription = x.ExtendedDescription,
                Place = x.Place,
                UnitPrice = x.UnitPrice,
                GenreId = x.GenreId,
                Genre = x.Genre.Name,
                DateEvent = x.DateEvent.ToShortDateString(),
                TimeEvent = x.DateEvent.ToShortTimeString(),
                ImageUrl = x.ImageUrl,
                TicketsQuantity = x.TicketsQuantity,
                Finalized = x.Finalized,
                Status = x.Status ? "Activo" : "Inactivo"
            })
            .AsQueryable();

        await httpContext.HttpContext.InsertarPaginacionHeader(queryable);
        var response = await queryable.OrderBy(x => x.Id).Paginate(pagination).ToListAsync();
        return response;
    }

    public async Task FinalizeAsync(int id)
    {
        var entity = await GetAsync(id);
        if (entity is not null)
        {
            entity.Finalized = true;
            await UpdateAsync();
        }
    }
}



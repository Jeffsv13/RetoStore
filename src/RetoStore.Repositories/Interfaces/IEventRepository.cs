using RetoStore.Dto.Request;
using RetoStore.Entities;
using RetoStore.Entities.Info;

namespace RetoStore.Repositories.Interfaces;

public interface IEventRepository : IRepositoryBase<Event>
{
    Task<ICollection<EventInfo>> GetAsync(string? title, PaginationDto pagination);
    Task FinalizeAsync(int id);
    Task<Event?> GetAsyncById(int id);
}

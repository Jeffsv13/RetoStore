using RetoStore.Entities;
using RetoStore.Persistence;
using RetoStore.Repositories.Interfaces;

namespace RetoStore.Repositories.Implementations;

public class GenreRepository : RepositoryBase<Genre>, IGenreRepository
{
    public GenreRepository(ApplicationDbContext context) : base(context)
    {
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RetoStore.Entities;

namespace RetoStore.Persistence.Seeders;

public class GenreSeeder
{
    private readonly IServiceProvider serviceProvider;
    public GenreSeeder(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public async Task SeedAsync()
    {
        //Obtener el contexto de la base de datos del servicio
        using (var context = serviceProvider.GetRequiredService<ApplicationDbContext>())
        {
            // Definimos los generos
            var listGenres = new List<Genre>
            {
                new Genre { Name = "Salsa" },
                new Genre { Name = "Rock" },
            };
            //Obtener los nombres de los generos que quieres añadir
            var genreNamesToAdd = listGenres.Select(x => x.Name).ToHashSet();

            //Obtener los nombres de los generos existentes en la base de datos
            var existingGenres = await context.Set<Genre>()
                .Where(g => genreNamesToAdd.Contains(g.Name))
                .Select(g => g.Name)
                .ToListAsync();

            //Filtrar los generos que no están en la base de datos
            var genresToAdd = listGenres
                .Where(g => !existingGenres.Contains(g.Name))
                .ToList();

            //Añadir los géneros que no existen en la base de datos
            if (genresToAdd.Any())
            {
                await context.Set<Genre>().AddRangeAsync(genresToAdd);
                await context.SaveChangesAsync();
            }
        }
    }
}

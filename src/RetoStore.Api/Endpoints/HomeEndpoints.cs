using RetoStore.Dto.Request;
using RetoStore.Services.Interfaces;

namespace RetoStore.Api.Endpoints;

public static class HomeEndpoints
{
    public static void MapHomeEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Home", async (IEventService eventService, IGenreService genreService) =>
        {
            var events = await eventService.GetAsync("", new PaginationDto
            {
                Page = 1,
                RecordsPerPage = 10
            });

            var genres = await genreService.GetAsync();

            return Results.Ok(new
            {
                Events = events.Data,
                Genres = genres.Data,
                Success = true
            });
        }).WithDescription("Permite mostrar los endpoints de la página principal");
    }
}

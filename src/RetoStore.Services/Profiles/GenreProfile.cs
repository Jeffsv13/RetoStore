using AutoMapper;
using RetoStore.Dto.Request;
using RetoStore.Dto.Response;
using RetoStore.Entities;

namespace RetoStore.Services.Profiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<Genre, GenreResponseDto>();
        CreateMap<GenreRequestDto, Genre>();
    }
}

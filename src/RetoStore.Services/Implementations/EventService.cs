using AutoMapper;
using Microsoft.Extensions.Logging;
using RetoStore.Dto.Request;
using RetoStore.Dto.Response;
using RetoStore.Entities;
using RetoStore.Repositories.Interfaces;
using RetoStore.Services.Interfaces;

namespace RetoStore.Services.Implementations;

public class EventService : IEventService
{
    private readonly IEventRepository repository;
    private readonly ILogger<EventService> logger;
    private readonly IMapper mapper;

    public EventService(
        IEventRepository repository,
        ILogger<EventService> logger,
        IMapper mapper
        )
    {
        this.repository = repository;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<BaseResponseGeneric<ICollection<EventResponseDto>>> GetAsync(string? title, PaginationDto pagination)
    {
        var response = new BaseResponseGeneric<ICollection<EventResponseDto>>();
        try
        {
            var data = await repository.GetAsync(title, pagination);
            response.Data = mapper.Map<ICollection<EventResponseDto>>(data);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al obtener la información";
            logger.LogError(ex, $"{response.ErrorMessage} {ex.Message}");
        }

        return response;
    }

    public async Task<BaseResponseGeneric<EventResponseDto>> GetAsync(int id)
    {
        var response = new BaseResponseGeneric<EventResponseDto>();
        try
        {
            //var data = await repository.GetAsync(id);
            var data = await repository.GetAsyncById(id);
            if (data is null)
            {
                response.ErrorMessage = $"No existe el evento con el id {id}";
                logger.LogWarning($"No existe el evento con el id {id}");
                return response;
            }
            response.Data = mapper.Map<EventResponseDto>(data);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al obtener la información";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
    public async Task<BaseResponseGeneric<int>> AddAsync(EventRequestDto request)
    {
        var response = new BaseResponseGeneric<int>();
        try
        {
            response.Data = await repository.AddAsync(mapper.Map<Event>(request));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al añadir la información";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> UpdateAsync(int id, EventRequestDto request)
    {
        var response = new BaseResponse();
        try
        {
            var data = await repository.GetAsync(id);
            if (data is null)
            {
                response.ErrorMessage = "El registro no fue encontrado";
                return response;
            }
            mapper.Map(request, data);
            await repository.UpdateAsync();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al actualizar la información";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
    public async Task<BaseResponse> DeleteAsync(int id)
    {
        var response = new BaseResponse();
        try
        {
            var data = await repository.GetAsync(id);
            if (data is null)
            {
                response.ErrorMessage = $"No existe el evento con el id {id}";
                logger.LogWarning($"No existe el evento con el id {id}");
                return response;
            }
            await repository.DeleteAsync(id);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al eliminar el registro";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> FinalizeAsync(int id)
    {
        var response = new BaseResponse();
        try
        {
            await repository.FinalizeAsync(id);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al finalizar el evento";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
}

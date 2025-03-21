﻿using AutoMapper;
using Microsoft.Extensions.Logging;
using RetoStore.Dto.Request;
using RetoStore.Dto.Response;
using RetoStore.Entities;
using RetoStore.Repositories.Interfaces;
using RetoStore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Services.Implementations;

public class GenreService : IGenreService
{
    private readonly IGenreRepository repository;
    private readonly ILogger<GenreService> logger;
    private readonly IMapper mapper;
    public GenreService(IGenreRepository repository, ILogger<GenreService> logger, IMapper mapper)
    {
        this.repository = repository;
        this.logger = logger;
        this.mapper = mapper;
    }

    public async Task<BaseResponseGeneric<ICollection<GenreResponseDto>>> GetAsync()
    {
        var response = new BaseResponseGeneric<ICollection<GenreResponseDto>>();
        try
        {
            var data = await repository.GetAsync();
            response.Data = mapper.Map<ICollection<GenreResponseDto>>(data);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al obtener la información";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<GenreResponseDto>> GetAsync(int id)
    {
        var response = new BaseResponseGeneric<GenreResponseDto>();
        try
        {
            response.Data = mapper.Map<GenreResponseDto>(await repository.GetAsync(id));
            response.Success = response.Data != null;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al obtener la información";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
    public async Task<BaseResponseGeneric<int>> AddAsync(GenreRequestDto request)
    {
        var response = new BaseResponseGeneric<int>();
        try
        {
            response.Data = await repository.AddAsync(mapper.Map<Genre>(request));
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al agregar el registro";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
    public async Task<BaseResponse> UpdateAsync(int id, GenreRequestDto request)
    {
        var response = new BaseResponse();
        try
        {
            var entity = await repository.GetAsync(id);
            if (entity is null)
            {
                response.ErrorMessage = $"No existe el género con el id {id}";
                logger.LogWarning(response.ErrorMessage);
                return response;
            }
            mapper.Map(request, entity);
            await repository.UpdateAsync();
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Ocurrió un error al actualizar el registro";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
    public async Task<BaseResponse> DeleteAsync(int id)
    {
        var response = new BaseResponse();
        try
        {
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
}

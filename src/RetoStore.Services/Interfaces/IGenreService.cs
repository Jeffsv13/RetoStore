﻿using RetoStore.Dto.Request;
using RetoStore.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Services.Interfaces;

public interface IGenreService
{
    Task<BaseResponseGeneric<ICollection<GenreResponseDto>>> GetAsync();
    Task<BaseResponseGeneric<GenreResponseDto>> GetAsync(int id);
    Task<BaseResponseGeneric<int>> AddAsync(GenreRequestDto request);
    Task<BaseResponse> UpdateAsync(int id, GenreRequestDto request);
    Task<BaseResponse> DeleteAsync(int id);
}

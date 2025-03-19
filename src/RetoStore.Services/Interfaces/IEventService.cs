using RetoStore.Dto.Request;
using RetoStore.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Services.Interfaces;

public interface IEventService
{
    Task<BaseResponseGeneric<ICollection<EventResponseDto>>> GetAsync(string? title, PaginationDto pagination);
    Task<BaseResponseGeneric<EventResponseDto>> GetAsync(int id);
    Task<BaseResponseGeneric<int>> AddAsync(EventRequestDto request);
    Task<BaseResponse> UpdateAsync(int id, EventRequestDto request);
    Task<BaseResponse> DeleteAsync(int id);
    Task<BaseResponse> FinalizeAsync(int id);
}

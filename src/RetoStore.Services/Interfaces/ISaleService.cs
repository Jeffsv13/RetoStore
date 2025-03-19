using RetoStore.Dto.Request;
using RetoStore.Dto.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Services.Interfaces;

public interface ISaleService
{
    Task<BaseResponseGeneric<int>> AddAsync(string email, SaleRequestDto request);
    Task<BaseResponseGeneric<SaleResponseDto>> GetAsync(int id);
    Task<BaseResponseGeneric<ICollection<SaleResponseDto>>> GetAsync(SaleByDateSearchDto search, PaginationDto pagination);
    Task<BaseResponseGeneric<ICollection<SaleResponseDto>>> GetAsync(string email, string? title, PaginationDto pagination);
    Task<BaseResponseGeneric<ICollection<SaleReportResponseDto>>> GetSaleReportAsync(DateTime dateStart, DateTime dateEnd);
}

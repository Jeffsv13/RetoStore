using AutoMapper;
using RetoStore.Dto.Request;
using RetoStore.Dto.Response;
using RetoStore.Entities;
using RetoStore.Entities.Info;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Services.Profiles;

public class SaleProfile : Profile
{
    private static readonly CultureInfo culture = new("es-PE");//ddmmaaaa, etc

    public SaleProfile()
    {
        CreateMap<SaleRequestDto, Sale>()
            .ForMember(d => d.Quantity, o => o.MapFrom(x => x.TicketsQuantity));

        CreateMap<Sale, SaleResponseDto>()
            .ForMember(d => d.SaleId, o => o.MapFrom(x => x.Id))
            .ForMember(d => d.DateEvent, o => o.MapFrom(x => x.Event.DateEvent.ToString("D", culture)))
            .ForMember(d => d.TimeEvent, o => o.MapFrom(x => x.Event.DateEvent.ToString("T", culture)))
            .ForMember(d => d.Genre, o => o.MapFrom(x => x.Event.Genre.Name))
            .ForMember(d => d.ImageUrl, o => o.MapFrom(x => x.Event.ImageUrl))
            .ForMember(d => d.Title, o => o.MapFrom(x => x.Event.Title))
            .ForMember(d => d.Fullname, o => o.MapFrom(x => x.Customer.FullName))
            .ForMember(d => d.SaleDate, o => o.MapFrom(x => x.SaleDate.ToString("dd/MM/yyyy HH:mm", culture)));
        CreateMap<ReportInfo, SaleReportResponseDto>();
    }
}

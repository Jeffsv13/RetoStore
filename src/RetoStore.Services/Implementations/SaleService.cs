using AutoMapper;
using Microsoft.Extensions.Logging;
using RetoStore.Dto.Request;
using RetoStore.Dto.Response;
using RetoStore.Entities;
using RetoStore.Repositories.Interfaces;
using RetoStore.Services.Interfaces;

namespace RetoStore.Services.Implementations;

public class SaleService : ISaleService
{
    private readonly ISaleRepository repository;
    private readonly IEventRepository eventRepository;
    private readonly ICustomerRepository customerRepository;
    private readonly IMapper mapper;
    private readonly ILogger<SaleService> logger;

    public SaleService(
        ISaleRepository repository,
        IEventRepository eventRepository,
        ICustomerRepository customerRepository,
        IMapper mapper,
        ILogger<SaleService> logger)
    {
        this.repository = repository;
        this.eventRepository = eventRepository;
        this.customerRepository = customerRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<BaseResponseGeneric<int>> AddAsync(string email, SaleRequestDto request)
    {
        var response = new BaseResponseGeneric<int>();

        try
        {
            await repository.CreateTransactionAsync();
            var entity = mapper.Map<Sale>(request);
            var customer = await customerRepository.GetByEmailAsync(email);
            if (customer == null)
            {
                throw new InvalidOperationException($"La cuenta {email} no esta registrada como cliente");

            }

            entity.CustomerId = customer.Id;

            var evento = await eventRepository.GetAsync(request.EventId);
            if (evento == null)
                throw new Exception($"El evento con el Id {request.EventId} no existe.");

            if (DateTime.Today >= evento.DateEvent)
                throw new InvalidOperationException($"No se puede comprar tickets para un evento {evento.Title} porque ya pasó.");

            if (evento.Finalized)
                throw new Exception($"El evento con el id {request.EventId} ya finalizó.");

            entity.Total = entity.Quantity * (decimal)evento.UnitPrice;

            await repository.AddAsync(entity);
            await repository.UpdateAsync();

            response.Data = entity.Id;
            response.Success = true;

            logger.LogInformation("Se creó correctamente la venta para {email}", email);
        }
        catch (InvalidOperationException ex)
        {
            await repository.RollBackAsync();
            response.ErrorMessage = "Seleccionó datos invalidos para su compra";

            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        catch (Exception ex)
        {
            await repository.RollBackAsync();
            response.ErrorMessage = "Error al crear la venta.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<SaleResponseDto>> GetAsync(int id)
    {
        var response = new BaseResponseGeneric<SaleResponseDto>();
        try
        {
            var sale = await repository.GetAsync(id);
            response.Data = mapper.Map<SaleResponseDto>(sale);
            response.Success = response.Data != null;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al seleccionar la venta.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<SaleResponseDto>>> GetAsync(SaleByDateSearchDto search, PaginationDto pagination)
    {
        var response = new BaseResponseGeneric<ICollection<SaleResponseDto>>();
        try
        {
            DateTime? dateInit = search.DateStart is not null ? Convert.ToDateTime(search.DateStart) : null;
            DateTime? dateEnd = search.DateEnd is not null ? Convert.ToDateTime(search.DateEnd) : null;

            var data = await repository.GetAsync(
                predicate: s =>
                (dateInit == null || s.SaleDate >= dateInit) &&
                (dateEnd == null || s.SaleDate <= dateEnd),
                orderBy: x => x.OperationNumber,
                pagination);
            response.Data = mapper.Map<ICollection<SaleResponseDto>>(data);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al filtrar las ventas por fechas.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
    public async Task<BaseResponseGeneric<ICollection<SaleResponseDto>>> GetAsync(string email, string? title, PaginationDto pagination)
    {
        var response = new BaseResponseGeneric<ICollection<SaleResponseDto>>();
        try
        {
            var data = await repository.GetAsync(
                predicate: s => s.Customer.Email == email && s.Event.Title.Contains(title ?? string.Empty),
                orderBy: x => x.SaleDate,
                pagination);
            response.Data = mapper.Map<ICollection<SaleResponseDto>>(data);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al filtrar las ventas del usuario por titulo.";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponseGeneric<ICollection<SaleReportResponseDto>>> GetSaleReportAsync(DateTime dateStart, DateTime dateEnd)
    {
        var response = new BaseResponseGeneric<ICollection<SaleReportResponseDto>>();
        try
        {
            //Codigo
            var list = await repository.GetSaleReportAsync(dateStart, dateEnd);
            response.Data = mapper.Map<ICollection<SaleReportResponseDto>>(list);
            response.Success = true;
        }
        catch (Exception ex)
        {

            response.ErrorMessage = "Error al obtener los datos del reporte";
            logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }
}


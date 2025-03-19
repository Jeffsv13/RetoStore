using Microsoft.AspNetCore.Http;
using RetoStore.Dto.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Dto.Request;

public class EventRequestDto
{
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ExtendedDescription { get; set; } = default!;
    public string Place { get; set; } = default!;
    public double UnitPrice { get; set; }
    public int GenreId { get; set; }
    public string DateEvent { get; set; } = default!;
    public string TimeEvent { get; set; } = default!;
    [FileSizeValidation(1)]
    [FileTypeValidation(FileTypeGroup.Image)]
    public IFormFile? Image { get; set; }
    public int TicketsQuantity { get; set; }
}

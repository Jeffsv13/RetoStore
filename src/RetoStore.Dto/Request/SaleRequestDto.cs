using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Dto.Request;

public class SaleRequestDto
{
    public int EventId { get; set; }
    public short TicketsQuantity { get; set; }
    public string Email { get; set; } = default!;
    public string FullName { get; set; } = default!;
}

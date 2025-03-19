using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Entities;

public class Sale : EntityBase
{
    public int CustomerId { get; set; }
    public int EventId { get; set; }
    public DateTime SaleDate { get; set; }
    public string OperationNumber { get; set; } = default!;
    public decimal Total { get; set; }
    public short Quantity { get; set; }

    // Navigation properties
    public Customer Customer { get; set; } = default!;
    public Event Event { get; set; } = default!;
}

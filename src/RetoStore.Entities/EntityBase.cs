using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Entities;

public class EntityBase
{
    public int Id { get; set; }
    public bool Status { get; set; } = true;
}

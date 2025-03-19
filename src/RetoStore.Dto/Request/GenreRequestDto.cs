using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Dto.Request;

public class GenreRequestDto
{
    public string Name { get; set; } = null!;
    public bool Status { get; set; } = true;
}

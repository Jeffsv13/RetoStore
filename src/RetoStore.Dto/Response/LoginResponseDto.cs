using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetoStore.Dto.Response;

public class LoginResponseDto
{
    public string Token { get; set; } = default!;
    public DateTime ExpirationDate { get; set; }
}

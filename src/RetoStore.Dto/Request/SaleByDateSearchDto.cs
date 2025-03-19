namespace RetoStore.Dto.Request;

public class SaleByDateSearchDto
{
    public string? DateStart { get; set; } = default!;
    public string? DateEnd { get; set; } = default!;
}

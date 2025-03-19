namespace RetoStore.Dto.Response;

public class RegisterResponseDto : LoginResponseDto
{
    public string UserId { get; set; } = default!;
}

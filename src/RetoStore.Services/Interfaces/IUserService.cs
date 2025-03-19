using RetoStore.Dto.Request;
using RetoStore.Dto.Response;

namespace RetoStore.Services.Interfaces;

public interface IUserService
{
    Task<BaseResponseGeneric<RegisterResponseDto>> RegisterAsync(RegisterRequestDto request);
    Task<BaseResponseGeneric<LoginResponseDto>> LoginAsync(LoginRequestDto request);
    Task<BaseResponse> RequestTokenToResetPasswordAsync(ResetPasswordRequestDto request);
    Task<BaseResponse> ResetPasswordAsync(NewPasswordRequestDto request);
    Task<BaseResponse> ChangePasswordAsync(string email, ChangePasswordRequestDto request);
}

using LibraryEcom.Application.DTOs.EmailConfirmation;
using LibraryEcom.Application.DTOs.Identity;

namespace LibraryEcom.Application.Interfaces.Services.Identity;

public interface IAuthenticationService
{
    Task<ResetPasswordRequestDto> ResetUserPassword(ResetUserPasswordDto resetUserPassword);

    void ExpireToken(string token);

    bool IsTokenExpired(string token);
}
using System.Security.Claims;
using EspCid.Application.DTO;

namespace EspCid.Application.Interfaces;

public interface IJwtService
{
    public int GetRefreshTokenMinutes();
    UserLoginOkResponse CreateAccessToken(IEnumerable<Claim> claims);
    string CreateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    string GetEmailFromToken(string token);
    string CreateForgotPasswordToken(string userEmail);
    bool ValidateToken(string token);
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EspCid.Application.DTO;
using EspCid.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EspCid.Application.Services;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public int? RefreshTokenMinutes;

    public int GetRefreshTokenMinutes()
    {
        RefreshTokenMinutes ??= int.Parse(configuration.GetSection("JWT:RefreshTokenValidityInMinutes").Value!);
        return RefreshTokenMinutes.Value;
    }

    public UserLoginOkResponse CreateAccessToken(IEnumerable<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            configuration.GetSection("JWT:Token").Value
            ?? throw new ArgumentException("Invalid Token Key!")));
        var claimlist = claims.ToList();
        if (claimlist.Count == 0) throw new ArgumentException("User has no claims.");

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var expiresMinutes = int.Parse(configuration.GetSection("JWT:TokenValidityInMinutes").Value!);
        var expiration = DateTime.UtcNow.AddMinutes(expiresMinutes);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claimlist),
            Expires = expiration,
            SigningCredentials = signingCredentials
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        var loginResponse = new UserLoginOkResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshToken = CreateRefreshToken(),
            Expiration = expiration
        };
        return loginResponse;
    }

    public string CreateRefreshToken()
    {
        var secureRandomBytes = new byte[128];
        var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(secureRandomBytes);
        var refreshToken = Convert.ToBase64String(secureRandomBytes);
        return refreshToken;
    }

    public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            configuration.GetSection("JWT:Token").Value
            ?? throw new ArgumentException("Invalid Token Key!")));
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateLifetime = false,
            ValidateAudience = false,
            ValidateIssuer = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters,
            out var securityToken);
        if (securityToken is not JwtSecurityToken) throw new SecurityTokenException("Invalid token!");

        return principal;
    }

    public string GetEmailFromToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);
            var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "email")
                            ?? throw new Exception("Email não encontrado!");

            return nameClaim.Value;
        }
        catch (Exception)
        {
            throw new Exception("Sessão inválida");
        }
    }

    public string CreateForgotPasswordToken(string userEmail)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            configuration.GetSection("JWT:Token").Value
            ?? throw new ArgumentException("Sessão inválida")));

        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, userEmail)
        };

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var expiresMinutes = int.Parse(configuration.GetSection("JWT:RecoveryPasswordToken").Value!);
        var expiration = DateTime.UtcNow.AddMinutes(expiresMinutes);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiration,
            SigningCredentials = signingCredentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            if (!(jwtToken.ValidTo < DateTime.UtcNow)) 
                return true;
        }
        catch (Exception)
        {
            throw new Exception("Sessão inválida");
        }

        return false;
    }
}
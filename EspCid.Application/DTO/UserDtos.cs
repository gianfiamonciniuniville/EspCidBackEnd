using EspCid.Domain.Entities;

namespace EspCid.Application.DTO;

public class UserDto(User user)
{
    public int Id { get; set; } = user.Id;
    public string UserName { get; set; } = user.UserName;
    public string Email { get; set; } = user.Email;
    public string? ProfileImageUrl { get; set; } = user.ProfileImageUrl;
    public string? Bio { get; set; } = user.Bio;
    public string Role { get; set; } = user.Role;
}

public class UserShortDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
}

public class RegisterUserDto
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class LoginUserDto
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class UpdateUserProfileDto
{
    public string? Bio { get; set; }
    public string? ProfileImageUrl { get; set; }
}

public class ChangeUserPasswordDto
{
    public string OldPassword { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}

public class AuthResponseDto(string token, UserDto user)
{
    public string Token { get; set; } = token;
    public UserDto User { get; set; } = user;
}
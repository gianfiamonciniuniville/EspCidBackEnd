using EspCid.Domain.Entities;

namespace EspCid.Application.DTO;

public class UserDto(User user)
{
    public int Id { get; set; } = user.Id;
    public string FirstName { get; set; } = user.FirstName;
    public string LastName { get; set; } = user.LastName;
    public string Document { get; set; } = user.Document;
    public string Phone { get; set; } = user.Phone;
    public string Email { get; set; } = user.Email;
    public string? ProfileImageUrl { get; set; } = user.ProfileImageUrl;
    public string? Bio { get; set; } = user.Bio;
    public string Role { get; set; } = user.Role;
}

public class UserShortDto
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
}

public class RegisterUserDto
{
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Document { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
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
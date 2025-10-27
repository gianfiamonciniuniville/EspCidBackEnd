using EspCid.Application.DTO;

namespace EspCid.Application.Interfaces;

public interface IUserService
{
    Task<AuthResponseDto> RegisterUserAsync(RegisterUserDto registerUserDto);
    Task<AuthResponseDto> LoginUserAsync(LoginUserDto loginUserDto);
    Task<UserDto> UpdateUserProfileAsync(int userId, UpdateUserProfileDto updateUserProfileDto);
    // Task ChangeUserPasswordAsync(int userId, ChangeUserPasswordDto changeUserPasswordDto);
    Task<UserDto?> GetUserByIdAsync(int id);
}
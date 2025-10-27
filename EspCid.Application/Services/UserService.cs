using EspCid.Application.DTO;
using EspCid.Application.Interfaces;
using EspCid.Domain.Entities;
using EspCid.Domain.Interfaces;
using System.Threading.Tasks;

namespace EspCid.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<AuthResponseDto> RegisterUserAsync(RegisterUserDto registerUserDto)
    {
        if (await _userRepository.GetByEmailAsync(registerUserDto.Email) != null)
            throw new Exception("User with this email already exists.");
        if (await _userRepository.GetByUserNameAsync(registerUserDto.UserName) != null)
            throw new Exception("User with this username already exists.");

        // TODO: Hash password
        var passwordHash = registerUserDto.Password;

        var user = new User
        {
            UserName = registerUserDto.UserName,
            Email = registerUserDto.Email,
            PasswordHash = passwordHash,
            Role = "User"
        };

        await _userRepository.CreateAsync(user);
        
        // TODO: Generate JWT token
        var token = "fake_token";

        return new AuthResponseDto(token, new UserDto(user));
    }

    public async Task<AuthResponseDto> LoginUserAsync(LoginUserDto loginUserDto)
    {
        var user = await _userRepository.GetByEmailAsync(loginUserDto.Email);
        if (user == null)
        {
            throw new Exception("Invalid credentials.");
        }

        // TODO: Verify password
        if (user.PasswordHash != loginUserDto.Password)
        {
            throw new Exception("Invalid credentials.");
        }

        // TODO: Generate JWT token
        var token = "fake_token";

        return new AuthResponseDto(token, new UserDto(user));
    }

    public async Task<UserDto> UpdateUserProfileAsync(int userId, UpdateUserProfileDto updateUserProfileDto)
    {
        var user = await _userRepository.GetByIdAsync(userId)
            ?? throw new Exception("User not found.");

        user.Bio = updateUserProfileDto.Bio;
        user.ProfileImageUrl = updateUserProfileDto.ProfileImageUrl;

        await _userRepository.UpdateAsync(user);

        return new UserDto(user);
    }

    // public async Task ChangeUserPasswordAsync(int userId, ChangeUserPasswordDto changeUserPasswordDto)
    // {
    //     var user = await _userRepository.GetByIdAsync(userId);
    //     if (user == null)
    //     { 
    //         throw new Exception("User not found.");
    //     }
    //
    //     // TODO: Verify old password
    //     if (user.PasswordHash != changeUserPasswordDto.OldPassword)
    //     {
    //         throw new Exception("Invalid old password.");
    //     }
    //
    //     // TODO: Hash new password
    //     user.PasswordHash = changeUserPasswordDto.NewPassword;
    //
    //     await _userRepository.UpdateAsync(user);
    // }

    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user == null ? null : new UserDto(user);
    }
}

using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EspCid.Application.DTO;
using EspCid.Application.Interfaces;

namespace EspCid.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    [EndpointSummary("Registra um novo usuário")]
    [EndpointDescription("Registra um novo usuário no sistema com as informações fornecidas.")]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        try
        {
            var authResponse = await _userService.RegisterUserAsync(registerUserDto);
            return Ok(authResponse);
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }   
    }

    [HttpPost("login")]
    [EndpointSummary("Autentica um usuário")]
    [EndpointDescription("Autentica um usuário e retorna um token de acesso.")]
    public async Task<IActionResult> Login(LoginUserDto loginUserDto)
    {
        try
        {
            var authResponse = await _userService.LoginUserAsync(loginUserDto);
            return Ok(authResponse);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("profile/{id}")]
    [EndpointSummary("Atualiza o perfil de um usuário")]
    [EndpointDescription("Atualiza as informações de perfil de um usuário existente.")]
    public async Task<IActionResult> UpdateProfile(int id, UpdateUserProfileDto updateUserProfileDto)
    {
        try
        {
            var user = await _userService.UpdateUserProfileAsync(id, updateUserProfileDto);
            return Ok(user);
        }catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }

    // [HttpPut("change-password")]
    // // [Authorize]
    // public async Task<IActionResult> ChangePassword(ChangeUserPasswordDto changeUserPasswordDto)
    // {
    //     // var userId = ... // get from token
    //     var userId = 1;
    //     await _userService.ChangeUserPasswordAsync(userId, changeUserPasswordDto);
    //     return Ok();
    // }

    [HttpGet("{id}")]
    [EndpointSummary("Obtém um usuário pelo ID")]
    [EndpointDescription("Obtém os detalhes de um usuário específico pelo seu ID.")]
    public async Task<IActionResult> GetUser(int id)
    {
        try
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        catch(Exception ex){
            return BadRequest(ex.Message);
        }
    }
}

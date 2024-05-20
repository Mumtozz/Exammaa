using Domain;
using Domain.Dtos.UserDto;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(IUserService context) : ControllerBase
{
    [HttpGet("get-Users")]
    public async  Task<PagedResponse<List<GetUserDto>>> GetUserAsync([FromQuery]UserFilter filter)
    {
        return await context.GetUser(filter);
    }

    [HttpPost("create-User")]
    public async Task<Response<string>> AddUserAsync([FromForm]AddUserDto User)
    {
        return await context.AddUser(User);
    }

    [HttpPut("update-User")]
    public async Task<Response<string>> UpdateUserAsync([FromForm]UpdateUserDto User)
    {
        return await context.UpdateUser(User);
    }

    [HttpDelete("Delete User")]
    public async Task<Response<bool>> DeleteUserAsync(int id)
    {
        return await context.DeleteUser(id);
    }

    [HttpGet("get-User-By-Id")]
    public async Task<Response<GetUserDto>> GetUserById(int id)
    {
        return await context.GetUserById(id);
    }
}
using Domain.Dtos.UserDto;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Services.UserService;

public interface IUserService
{
    Task<PagedResponse<List<GetUserDto>>> GetUser(UserFilter filter);
    Task<Response<string>> AddUser(AddUserDto addUserDto);
    Task<Response<string>> UpdateUser(UpdateUserDto updateUserDto);
    Task<Response<bool>> DeleteUser(int id);
    Task<Response<GetUserDto>> GetUserById(int id);
}

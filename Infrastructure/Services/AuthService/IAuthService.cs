using Domain.DTOs.AuthDto;
using Domain.Response;

namespace Infrastructure.Services.AuthService;

public interface IAuthService
{
    public Task<Response<string>> Login(LoginDto loginDto);
}

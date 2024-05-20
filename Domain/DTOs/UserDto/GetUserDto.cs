using Domain.Enum;

namespace Domain.Dtos.UserDto;

public class GetUserDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public DateTime DateOfRegister { get; set; }
    public Role Role { get; set; }
}

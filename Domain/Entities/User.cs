using Domain.Enum;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? HashPassword { get; set; }
    public DateTime DateOfRegister { get; set; }
    public Role Role { get; set; }

    public List<Payment>? Payments { get; set; }
    public List<Membership>? Memberships { get; set; }
}

using Domain.Enum;

namespace Domain.Dtos.MembershipDto;

public class GetMembershipDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public Types? Type { get; set; }
    public decimal? Price { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? Photo { get; set; }
}

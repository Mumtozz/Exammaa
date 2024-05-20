using Domain.Enum;

namespace Domain.Entities;

public class Payment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime? Data { get; set; }
    public Status? Status { get; set; }


    public User? User { get; set; }
}

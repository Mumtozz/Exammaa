using Domain.Enum;

namespace Domain.Dtos.PaymentDto;

public class UpdatePaymentDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public DateTime? Data { get; set; }
    public Status? Status { get; set; }
}

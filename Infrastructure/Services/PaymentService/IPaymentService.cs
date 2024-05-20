using Domain.Dtos.PaymentDto;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Services.PaymentService;

public interface IPaymentService
{
    Task<PagedResponse<List<GetPaymentDto>>> GetPayment(PaymentFilter filter);
    Task<Response<string>> AddPayment(AddPaymentDto addPaymentDto);
    Task<Response<string>> UpdatePayment(UpdatePaymentDto updatePaymentDto);
    Task<Response<bool>> DeletePayment(int id);
    Task<Response<GetPaymentDto>> GetPaymentById(int id);
}

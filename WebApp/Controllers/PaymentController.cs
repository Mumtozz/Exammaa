using Domain;
using Domain.Dtos.PaymentDto;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Services.PaymentService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class PaymentController(IPaymentService context) : ControllerBase
{
    [HttpGet("get-Payments")]
    public async  Task<PagedResponse<List<GetPaymentDto>>> GetPaymentAsync([FromQuery]PaymentFilter filter)
    {
        return await context.GetPayment(filter);
    }

    [HttpPost("create-Payment")]
    public async Task<Response<string>> AddPaymentAsync([FromForm]AddPaymentDto Payment)
    {
        return await context.AddPayment(Payment);
    }

    [HttpPut("update-Payment")]
    public async Task<Response<string>> UpdatePaymentAsync([FromForm]UpdatePaymentDto Payment)
    {
        return await context.UpdatePayment(Payment);
    }

    [HttpDelete("Delete Payment")]
    public async Task<Response<bool>> DeletePaymentAsync(int id)
    {
        return await context.DeletePayment(id);
    }

    [HttpGet("get-Payment-By-Id")]
    public async Task<Response<GetPaymentDto>> GetPaymentById(int id)
    {
        return await context.GetPaymentById(id);
    }
}
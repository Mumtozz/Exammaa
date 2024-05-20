using System.Net;
using AutoMapper;
using Domain;
using Domain.Dtos.PaymentDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.PaymentService;

public class PaymentService : IPaymentService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public PaymentService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<Response<string>> AddPayment(AddPaymentDto addPaymentDto)
    {
        try
        {
            var mapped = _mapper.Map<Payment>(addPaymentDto);
            await _context.Payments.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Payment added successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetPaymentDto>>> GetPayment(PaymentFilter filter)
    {
        try
        {
            var Payments = _context.Payments.AsQueryable();
    
            var Payment = await Payments.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await Payments.CountAsync();

            var response = _mapper.Map<List<GetPaymentDto>>(Payments);
            return new PagedResponse<List<GetPaymentDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetPaymentDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeletePayment(int id)
    {
        try
        {
            var existing = await _context.Payments.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (existing == 0) return new Response<bool>(HttpStatusCode.BadRequest, "Payment not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> UpdatePayment(UpdatePaymentDto updatePaymentDto)
    {
        try
        {
            var existing = await _context.Payments.AnyAsync(e => e.Id == updatePaymentDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Payment not found!");
            var mapped = _mapper.Map<Payment>(updatePaymentDto);
            _context.Payments.Update(mapped);

            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetPaymentDto>> GetPaymentById(int id)
    {
        try
        {
            var existing = await _context.Payments.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetPaymentDto>(HttpStatusCode.BadRequest, "Payment not found");
            var Payment = _mapper.Map<GetPaymentDto>(existing);
            return new Response<GetPaymentDto>(Payment);
        }
        catch (Exception e)
        {
            return new Response<GetPaymentDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}

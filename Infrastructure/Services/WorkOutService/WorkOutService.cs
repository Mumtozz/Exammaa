using System.Net;
using AutoMapper;
using Domain;
using Domain.Dtos;
using Domain.Dtos.WorkOut;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.WorkOutService;

public class WorkOutService : IWorkOutService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public WorkOutService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<Response<string>> AddWorkOut(AddWorkOutDto addWorkOutDto)
    {
        try
        {
            var mapped = _mapper.Map<WorkOut>(addWorkOutDto);
            await _context.WorkOuts.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("WorkOut added successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetWorkOutDto>>> GetWorkOut(WorkOutFilter filter)
    {
        try
        {
            var WorkOuts = _context.WorkOuts.AsQueryable();
            if (!string.IsNullOrEmpty(filter.Name))
                WorkOuts = WorkOuts.Where(x => x.Name.ToLower().Contains(filter.Name.ToLower()));

            var WorkOut = await WorkOuts.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await WorkOuts.CountAsync();

            var response = _mapper.Map<List<GetWorkOutDto>>(WorkOuts);
            return new PagedResponse<List<GetWorkOutDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetWorkOutDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteWorkOut(int id)
    {
        try
        {
            var existing = await _context.WorkOuts.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (existing == 0) return new Response<bool>(HttpStatusCode.BadRequest, "WorkOut not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> UpdateWorkOut(UpdateWorkOutDto updateWorkOutDto)
    {
        try
        {
            var existing = await _context.WorkOuts.AnyAsync(e => e.Id == updateWorkOutDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "WorkOut not found!");
            var mapped = _mapper.Map<WorkOut>(updateWorkOutDto);
            _context.WorkOuts.Update(mapped);

            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetWorkOutDto>> GetWorkOutById(int id)
    {
        try
        {
            var existing = await _context.WorkOuts.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetWorkOutDto>(HttpStatusCode.BadRequest, "WorkOut not found");
            var WorkOut = _mapper.Map<GetWorkOutDto>(existing);
            return new Response<GetWorkOutDto>(WorkOut);
        }
        catch (Exception e)
        {
            return new Response<GetWorkOutDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}

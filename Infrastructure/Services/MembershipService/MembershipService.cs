using System.Net;
using AutoMapper;
using Domain;
using Domain.Dtos.MembershipDto;
using Domain.Entities;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services.MembershipService;

public class MembershipService : IMembershipService
{
    private readonly IMapper _mapper;
    private readonly DataContext _context;

    public MembershipService(IMapper mapper, DataContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<Response<string>> AddMembership(AddMembershipDto addMembershipDto)
    {
        try
        {
            var mapped = _mapper.Map<Membership>(addMembershipDto);
            await _context.Memberships.AddAsync(mapped);
            await _context.SaveChangesAsync();
            return new Response<string>("Membership added successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<PagedResponse<List<GetMembershipDto>>> GetMembership(MembershipFilter filter)
    {
        try
        {
            var Memberships = _context.Memberships.AsQueryable();

            var Membership = await Memberships.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize).ToListAsync();
            var total = await Memberships.CountAsync();

            var response = _mapper.Map<List<GetMembershipDto>>(Memberships);
            return new PagedResponse<List<GetMembershipDto>>(response, total, filter.PageNumber, filter.PageSize);
        }
        catch (Exception e)
        {
            return new PagedResponse<List<GetMembershipDto>>(HttpStatusCode.InternalServerError, e.Message);
        }
    }

    public async Task<Response<bool>> DeleteMembership(int id)
    {
        try
        {
            var existing = await _context.Memberships.Where(e => e.Id == id).ExecuteDeleteAsync();
            if (existing == 0) return new Response<bool>(HttpStatusCode.BadRequest, "Membership not found!");
            return new Response<bool>(true);
        }
        catch (Exception ex)
        {
            return new Response<bool>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }


    public async Task<Response<string>> UpdateMembership(UpdateMembershipDto updateMembershipDto)
    {
        try
        {
            var existing = await _context.Memberships.AnyAsync(e => e.Id == updateMembershipDto.Id);
            if (!existing) return new Response<string>(HttpStatusCode.BadRequest, "Membership not found!");
            var mapped = _mapper.Map<Membership>(updateMembershipDto);
            _context.Memberships.Update(mapped);

            await _context.SaveChangesAsync();
            return new Response<string>("Updated successfully");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetMembershipDto>> GetMembershipById(int id)
    {
        try
        {
            var existing = await _context.Memberships.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null) return new Response<GetMembershipDto>(HttpStatusCode.BadRequest, "Membership not found");
            var Membership = _mapper.Map<GetMembershipDto>(existing);
            return new Response<GetMembershipDto>(Membership);
        }
        catch (Exception e)
        {
            return new Response<GetMembershipDto>(HttpStatusCode.InternalServerError, e.Message);
        }
    }
}

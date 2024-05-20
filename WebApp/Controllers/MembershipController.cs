using Domain;
using Domain.Dtos.MembershipDto;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Services.MembershipService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class MembershipController(IMembershipService context) : ControllerBase
{
    [HttpGet("get-Memberships")]
    public async  Task<PagedResponse<List<GetMembershipDto>>> GetMembershipAsync([FromQuery]MembershipFilter filter)
    {
        return await context.GetMembership(filter);
    }

    [HttpPost("create-Membership")]
    public async Task<Response<string>> AddMembershipAsync([FromForm]AddMembershipDto Membership)
    {
        return await context.AddMembership(Membership);
    }

    [HttpPut("update-Membership")]
    public async Task<Response<string>> UpdateMembershipAsync([FromForm]UpdateMembershipDto Membership)
    {
        return await context.UpdateMembership(Membership);
    }

    [HttpDelete("Delete Membership")]
    public async Task<Response<bool>> DeleteMembershipAsync(int id)
    {
        return await context.DeleteMembership(id);
    }

    [HttpGet("get-Membership-By-Id")]
    public async Task<Response<GetMembershipDto>> GetMembershipById(int id)
    {
        return await context.GetMembershipById(id);
    }
}
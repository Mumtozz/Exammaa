using Domain;
using Domain.Dtos.WorkOut;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Services.WorkOutService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WorkOutController(IWorkOutService context) : ControllerBase
{
    [HttpGet("get-WorkOuts")]
    public async  Task<PagedResponse<List<GetWorkOutDto>>> GetWorkOutAsync([FromQuery]WorkOutFilter filter)
    {
        return await context.GetWorkOut(filter);
    }

    [HttpPost("create-WorkOut")]
    public async Task<Response<string>> AddWorkOutAsync([FromForm]AddWorkOutDto WorkOut)
    {
        return await context.AddWorkOut(WorkOut);
    }

    [HttpPut("update-WorkOut")]
    public async Task<Response<string>> UpdateWorkOutAsync([FromForm]UpdateWorkOutDto WorkOut)
    {
        return await context.UpdateWorkOut(WorkOut);
    }

    [HttpDelete("Delete WorkOut")]
    public async Task<Response<bool>> DeleteWorkOutAsync(int id)
    {
        return await context.DeleteWorkOut(id);
    }

    [HttpGet("get-WorkOut-By-Id")]
    public async Task<Response<GetWorkOutDto>> GetWorkOutById(int id)
    {
        return await context.GetWorkOutById(id);
    }
}
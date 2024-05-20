using Domain.Dtos.ClassScheduleDto;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Services.ClassScheduleService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ClassScheduleController(IClassScheduleService context) : ControllerBase
{
    [HttpGet("get-ClassSchedules")]
    public async  Task<PagedResponse<List<GetClassScheduleDto>>> GetClassScheduleAsync([FromQuery]ClassScheduleFilter filter)
    {
        return await context.GetClassSchedule(filter);
    }

    [HttpPost("create-ClassSchedule")]
    public async Task<Response<string>> AddClassScheduleAsync([FromForm]AddClassScheduleDto ClassSchedule)
    {
        return await context.AddClassSchedule(ClassSchedule);
    }

    [HttpPut("update-ClassSchedule")]
    public async Task<Response<string>> UpdateClassScheduleAsync([FromForm]UpdateClassScheduleDto ClassSchedule)
    {
        return await context.UpdateClassSchedule(ClassSchedule);
    }

    [HttpDelete("Delete ClassSchedule")]
    public async Task<Response<bool>> DeleteClassScheduleAsync(int id)
    {
        return await context.DeleteClassSchedule(id);
    }

    [HttpGet("get-ClassSchedule-By-Id")]
    public async Task<Response<GetClassScheduleDto>> GetClassScheduleById(int id)
    {
        return await context.GetClassScheduleById(id);
    }
}
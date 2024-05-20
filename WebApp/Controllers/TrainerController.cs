using Domain.Dtos.TrainerDto;
using Domain.Filters;
using Domain.Response;
using Infrastructure.Services.TrainerService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TrainerController(ITrainerService context) : ControllerBase
{
    [HttpGet("get-Trainers")]
    public async  Task<PagedResponse<List<GetTrainerDto>>> GetTrainerAsync([FromQuery]TrainerFilter filter)
    {
        return await context.GetTrainer(filter);
    }

    [HttpPost("create-Trainer")]
    public async Task<Response<string>> AddTrainerAsync([FromForm]AddTrainerDto Trainer)
    {
        return await context.AddTrainer(Trainer);
    }

    [HttpPut("update-Trainer")]
    public async Task<Response<string>> UpdateTrainerAsync([FromForm]UpdateTrainerDto Trainer)
    {
        return await context.UpdateTrainer(Trainer);
    }

    [HttpDelete("Delete Trainer")]
    public async Task<Response<bool>> DeleteTrainerAsync(int id)
    {
        return await context.DeleteTrainer(id);
    }

    [HttpGet("get-Trainer-By-Id")]
    public async Task<Response<GetTrainerDto>> GetTrainerById(int id)
    {
        return await context.GetTrainerById(id);
    }
}
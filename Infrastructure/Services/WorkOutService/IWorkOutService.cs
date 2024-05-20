using Domain.Dtos.WorkOut;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Services.WorkOutService;

public interface IWorkOutService
{
    Task<PagedResponse<List<GetWorkOutDto>>> GetWorkOut(WorkOutFilter filter);
    Task<Response<string>> AddWorkOut(AddWorkOutDto addWorkOutDto);
    Task<Response<string>> UpdateWorkOut(UpdateWorkOutDto updateWorkOutDto);
    Task<Response<bool>> DeleteWorkOut(int id);
    Task<Response<GetWorkOutDto>> GetWorkOutById(int id);
}

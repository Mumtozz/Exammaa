using Domain.Dtos.ClassScheduleDto;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Services.ClassScheduleService;

public interface IClassScheduleService
{
    Task<PagedResponse<List<GetClassScheduleDto>>> GetClassSchedule(ClassScheduleFilter filter);
    Task<Response<string>> AddClassSchedule(AddClassScheduleDto addClassScheduleDto);
    Task<Response<string>> UpdateClassSchedule(UpdateClassScheduleDto updateClassScheduleDto);
    Task<Response<bool>> DeleteClassSchedule(int id);
    Task<Response<GetClassScheduleDto>> GetClassScheduleById(int id);
}

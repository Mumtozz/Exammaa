using Domain.Dtos.TrainerDto;
using Domain.Filters;
using Domain.Response;

namespace Infrastructure.Services.TrainerService;

public interface ITrainerService
{
    Task<PagedResponse<List<GetTrainerDto>>> GetTrainer(TrainerFilter filter);
    Task<Response<string>> AddTrainer(AddTrainerDto addTrainerDto);
    Task<Response<string>> UpdateTrainer(UpdateTrainerDto updateTrainerDto);
    Task<Response<bool>> DeleteTrainer(int id);
    Task<Response<GetTrainerDto>> GetTrainerById(int id);
}

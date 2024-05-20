using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.TrainerDto;

public class AddTrainerDto
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public string? Specialization { get; set; }
    public IFormFile? Photo { get; set; }
}

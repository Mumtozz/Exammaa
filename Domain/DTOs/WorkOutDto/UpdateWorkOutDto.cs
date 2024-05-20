namespace Domain.Dtos.WorkOut;

public class UpdateWorkOutDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Duration { get; set; }
    public string? Intensity { get; set; }
}

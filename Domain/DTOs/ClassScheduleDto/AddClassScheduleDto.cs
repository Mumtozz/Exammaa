namespace Domain.Dtos.ClassScheduleDto;

public class AddClassScheduleDto
{
    public int? WorkOutId { get; set; }
    public int? TrainerId { get; set; }
    public DateTime? Date { get; set; }
    public int? Duration { get; set; }
    public string? Address { get; set; }
}

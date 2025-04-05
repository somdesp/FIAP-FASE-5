namespace FIAP.TECH.CORE.APPLICATION.DTO;

public class ScheduleUpdateDto
{
    public int Id { get; set; }
    public required int IdDoctor { get; set; }
    public int? IdPatient { get; set; }
}

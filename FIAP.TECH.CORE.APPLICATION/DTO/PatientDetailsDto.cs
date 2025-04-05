namespace FIAP.TECH.CORE.APPLICATION.DTO;

public class PatientDetailsDto
{
    public string Name { get; set; }
    public string CPF { get; set; }
    public bool IsActive { get; set; }
    public List<ScheduleDto>? Schedule { get; set; }
}
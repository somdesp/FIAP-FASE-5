namespace FIAP.TECH.CORE.APPLICATION.DTO;

public class DoctorDetailsDto
{
    public string Name { get; set; }
    public string CRM { get; set; }
    public bool IsActive { get; set; }
    public List<ScheduleDto>? Schedule { get; set; }
}
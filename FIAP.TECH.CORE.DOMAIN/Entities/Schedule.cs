namespace FIAP.TECH.CORE.DOMAIN.Entities;

public class Schedule : BaseEntity
{
    public required DateTime Date { get; set; }
    public bool HaveSchedule { get; set; }
    public int? ScheduledIdPatient { get; set; }
}

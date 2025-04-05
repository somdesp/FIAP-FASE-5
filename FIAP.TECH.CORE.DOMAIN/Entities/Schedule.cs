namespace FIAP.TECH.CORE.DOMAIN.Entities;

public class Schedule : BaseEntity
{
    public required DateTime Date { get; set; }
    public required bool HaveSchedule { get; set; }
    public required int IdDoctor { get; set; }
    public int? IdPatient { get; set; }


    public Doctor Doctor { get; set; }
    public Patient? Patient { get; set; }

}

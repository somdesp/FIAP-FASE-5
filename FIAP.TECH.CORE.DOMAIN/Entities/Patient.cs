namespace FIAP.TECH.CORE.DOMAIN.Entities;

public class Patient : BaseEntity
{
    public required string Name { get; set; }
    public required string CPF { get; set; }
    public required string Password { get; set; }
    public bool IsActive { get; set; }

    public List<Schedule> Schedule { get; set; }

}

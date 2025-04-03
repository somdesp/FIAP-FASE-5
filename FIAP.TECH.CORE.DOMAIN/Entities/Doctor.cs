namespace FIAP.TECH.CORE.DOMAIN.Entities;

public class Doctor : BaseEntity
{
    public required string Name { get; set; }
    public required string CRM { get; set; }
    public required string Password { get; set; }
    public bool IsActive { get; set; }
}

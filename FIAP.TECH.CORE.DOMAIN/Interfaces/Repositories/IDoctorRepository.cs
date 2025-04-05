using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;

public interface IDoctorRepository : IRepository<Doctor>
{
    Task<Doctor?> Authenticate(string CRM, string password);
}

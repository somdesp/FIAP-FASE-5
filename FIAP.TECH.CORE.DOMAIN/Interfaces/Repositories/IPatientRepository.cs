using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;

public interface IPatientRepository : IRepository<Patient>
{
    Task<Patient?> Authenticate(string CPF, string password);
}

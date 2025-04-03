using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;

public interface IUserRepository : IRepository<Patient>
{
    Task<Patient?> Authenticate(string email, string password);
}

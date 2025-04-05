using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.INFRASTRUCTURE.Repositories;

public class PatientRepository : Repository<Patient>, IPatientRepository
{
    private readonly AppDbContext _appDbContext;
    public PatientRepository(AppDbContext appDbContext) : base(appDbContext) =>
            _appDbContext = appDbContext;

    public async Task<Patient?> Authenticate(string CPF, string password)
    {
        return await _appDbContext.Patients.AsNoTracking().SingleOrDefaultAsync(x => x.CPF == CPF && x.Password == password && x.IsActive);
    }
}

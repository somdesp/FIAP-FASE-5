using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TECH.INFRASTRUCTURE.Repositories;

public class DoctorRepository : Repository<Doctor>, IDoctorRepository
{
    private readonly AppDbContext _appDbContext;
    public DoctorRepository(AppDbContext appDbContext) : base(appDbContext) =>
            _appDbContext = appDbContext;

    public Task<Doctor?> Authenticate(string CRM, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Doctor>> GetByDdd(int idDoctor)
    {
        return await _appDbContext.Doctors
            .AsNoTracking()
            .Include(c => c.Schedule)
            .Where(c => c.Id == idDoctor)
            .ToListAsync();
    }
}

using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Contexts;

namespace FIAP.TECH.INFRASTRUCTURE.Repositories;

public class ScheduleRepository : Repository<Schedule>, IScheduleRepository
{
    public ScheduleRepository(AppDbContext appDbContext) : base(appDbContext) { }
}

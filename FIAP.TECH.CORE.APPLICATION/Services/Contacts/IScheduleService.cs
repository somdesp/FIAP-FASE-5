using FIAP.TECH.CORE.APPLICATION.DTO;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Models;

namespace FIAP.TECH.CORE.APPLICATION.Services.Contacts;

public interface IScheduleService
{
    Task SendMessageAsync(Schedule message);
    Task Update(ScheduleUpdateDto contactDTO);
    Task Delete(int id);
    Task<ScheduleResponse> SendResponseMessageAsync(SearchBySpecialty contactByDDD);
}

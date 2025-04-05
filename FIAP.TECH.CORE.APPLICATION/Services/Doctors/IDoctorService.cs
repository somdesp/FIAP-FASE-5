using FIAP.TECH.CORE.APPLICATION.Authentication;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Models;

namespace FIAP.TECH.CORE.APPLICATION.Services.Doctors;

public interface IDoctorService
{
    Task<AuthenticateResponse> AuthenticateDoctor(AuthenticateRequestDoctor request);
    Task SendMessageAsync(Doctor doctor);
    Task<SpecialtyResponse> SendResponseMessageAsync(SearchBySpecialty contactByDDD);
}

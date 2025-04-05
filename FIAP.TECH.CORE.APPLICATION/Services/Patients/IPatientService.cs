using FIAP.TECH.CORE.APPLICATION.Authentication;
using FIAP.TECH.CORE.DOMAIN.Entities;

namespace FIAP.TECH.CORE.APPLICATION.Services.Patients;

public interface IPatientService
{
    Task<AuthenticateResponse?> AuthenticatePatient(AuthenticateRequestPatient request);
    Task SendMessageAsync(Patient patient);
}

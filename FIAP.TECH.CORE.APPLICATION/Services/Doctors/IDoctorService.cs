using FIAP.TECH.CORE.APPLICATION.Authentication;

namespace FIAP.TECH.CORE.APPLICATION.Services.Doctors;

public interface IDoctorService
{
    Task<AuthenticateResponse> AuthenticateDoctor(AuthenticateRequestDoctor request);
}

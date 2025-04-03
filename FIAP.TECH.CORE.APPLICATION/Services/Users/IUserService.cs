using FIAP.TECH.CORE.APPLICATION.Authentication;

namespace FIAP.TECH.CORE.APPLICATION.Services.Users;

public interface IUserService
{
    Task<AuthenticateResponse> AuthenticateDoctor(AuthenticateRequestDoctor request);
    Task<AuthenticateResponse?> AuthenticatePatient(AuthenticateRequestPatient request);
}

using AutoMapper;
using FIAP.TECH.CORE.APPLICATION.Authentication;
using FIAP.TECH.CORE.APPLICATION.Settings.JwtExtensions;
using FIAP.TECH.CORE.DOMAIN.Entities;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.CORE.DOMAIN.Models;
using MassTransit;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FIAP.TECH.CORE.APPLICATION.Services.Patients;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IMapper _mapper;
    private readonly IBusControl _busControl;
    private readonly IRequestClient<SearchBySpecialty> _requestClient;
    private readonly TokenSettings _tokenSettings;


    public PatientService(IPatientRepository patientRepositor,
        IBusControl busControl,
        IRequestClient<SearchBySpecialty> requestClient,
        IOptions<TokenSettings> tokenSettings)
    {
        _patientRepository = patientRepositor ?? throw new ArgumentNullException(nameof(_patientRepository));
        _tokenSettings = tokenSettings.Value;
        _busControl = busControl;
        _requestClient = requestClient;
    }

    #region Login
    public async Task<AuthenticateResponse?> AuthenticatePatient(AuthenticateRequestPatient request)
    {
        var user = await _patientRepository.Authenticate(request.CPF, request.Password);

        if (user is null) return null;

        var token = await GenerateJwtToken(user);

        return new AuthenticateResponse(user.Id, user.Name, token);
    }



    private async Task<string> GenerateJwtToken(Patient user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = await Task.Run(() =>
        {
            var key = Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()) }),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            return tokenHandler.CreateToken(tokenDescriptor);
        });

        return tokenHandler.WriteToken(token);
    }
    #endregion

    public async Task SendMessageAsync(Patient message)
    {
        try
        {
            await _busControl.Publish(message);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}

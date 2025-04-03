namespace FIAP.TECH.CORE.APPLICATION.Authentication;

public class AuthenticateRequestPatient
{
    public required string CPF { get; set; }

    public required string Password { get; set; }
}

public class AuthenticateRequestDoctor
{
    public required string CRM { get; set; }

    public required string Password { get; set; }
}


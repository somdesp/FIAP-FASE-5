namespace FIAP.TECH.CORE.APPLICATION.Authentication;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }

    public AuthenticateResponse(int id, string name, string token)
    {
        Id = id;
        Name = name;
        Token = token;
    }
}
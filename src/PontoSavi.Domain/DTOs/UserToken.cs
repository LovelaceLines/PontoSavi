namespace PontoSavi.Domain.DTOs;

public class UserToken
{
    public AuthToken AuthToken { get; set; }
    public UserDTO User { get; set; }

    public UserToken(AuthToken authToken, UserDTO user)
    {
        AuthToken = authToken;
        User = user;
    }
}
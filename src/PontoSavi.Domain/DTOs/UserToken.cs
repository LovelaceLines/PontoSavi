namespace PontoSavi.Domain.DTOs;

public class UserToken(AuthToken authToken, UserDTO user)
{
    public AuthToken AuthToken { get; set; } = authToken;
    public UserDTO User { get; set; } = user;
}
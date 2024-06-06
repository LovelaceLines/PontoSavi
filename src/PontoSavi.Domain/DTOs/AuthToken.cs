namespace PontoSavi.Domain.DTOs;

public class AuthToken
{
    public string TokenType { get; set; } = "Bearer";
    public string AccessToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public string RefreshToken { get; set; } = string.Empty;

    public AuthToken() { }

    public AuthToken(string accessToken, int expiresIn, string refreshToken, string? tokenType = null)
    {
        TokenType = tokenType ?? TokenType;
        AccessToken = accessToken;
        ExpiresIn = expiresIn;
        RefreshToken = refreshToken;
    }
}
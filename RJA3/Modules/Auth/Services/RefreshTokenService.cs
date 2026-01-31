using System.Security.Cryptography;

namespace RJA3.Modules.Auth.Services;

public interface IRefreshTokenService
{
    string GenerateRefreshToken();
}

public class RefreshTokenService : IRefreshTokenService
{
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}

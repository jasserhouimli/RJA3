using RJA3.Modules.Auth.Domain;

namespace RJA3.Modules.Auth.Services;

public interface IJwtTokenService
{
    string GenerateToken(ApplicationUser user);
}

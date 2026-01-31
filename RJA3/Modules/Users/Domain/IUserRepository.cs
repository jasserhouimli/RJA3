using System.Security.Claims;
using RJA3.Shared;

public interface IUserRepository
{
    Task<Result<UserDto>> GetMe(ClaimsPrincipal user);   
}


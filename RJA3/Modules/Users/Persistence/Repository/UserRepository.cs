using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using RJA3.Modules.Users.Persistence;
using RJA3.Shared;

public class UserRepository : IUserRepository
{
    private readonly UserDbContext _userDbContext;

    public UserRepository(UserDbContext userDbContext)
    {
        _userDbContext = userDbContext;
    }

    public async Task<Result<UserDto>> GetMe(ClaimsPrincipal userClaims)
    {
        var user = await _userDbContext.UserProfiles.AsNoTracking().FirstOrDefaultAsync(e => e.UserId == userClaims.GetUserId());

        if (user == null)
        {
            return Result<UserDto>.Failure("User not found.");
        }

        var userDto = new UserDto
        {
            Id = user.Id,
            Email = user.Email!,
            FullName = user.UserName!
        };

        return Result<UserDto>.Success(userDto);
    }
}
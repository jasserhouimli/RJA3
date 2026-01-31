using System.Security.Claims;
using RJA3.Shared;

public class GetMeHandler
{
    private readonly IUserRepository _userRepository;

    public GetMeHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Result<UserDto>> Handle(ClaimsPrincipal userClaims)
    {
        return await _userRepository.GetMe(userClaims);
    }

}
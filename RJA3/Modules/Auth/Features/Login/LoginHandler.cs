using Microsoft.AspNetCore.Identity;
using RJA3.Modules.Auth.Domain;
using RJA3.Modules.Auth.Persistence;
using RJA3.Modules.Auth.Services;
using RJA3.Shared;
namespace RJA3.Modules.Auth.Features.Login;

public class LoginHandler
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;

    public LoginHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IJwtTokenService jwtTokenService,
        IRefreshTokenService refreshTokenService,
        ApplicationDbContext dbContext,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand command)
    {
        var user = await _userManager.FindByEmailAsync(command.Email);
        if (user == null)
        {
            return Result<LoginResponse>.Failure("Invalid email or password.");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, lockoutOnFailure: false);
        if (!result.Succeeded)
        {
            return Result<LoginResponse>.Failure("Invalid email or password.");
        }

        var token = _jwtTokenService.GenerateToken(user);

        var refreshToken = _refreshTokenService.GenerateRefreshToken();
        var refreshTokenExpiryDays = int.Parse(_configuration["Jwt:RefreshTokenExpiryDays"] ?? "7");

        var refreshTokenEntity = new Domain.RefreshToken
        {
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpiryDays)
        };

        _dbContext.RefreshTokens.Add(refreshTokenEntity);
        await _dbContext.SaveChangesAsync();

        var response = new LoginResponse(
            AccessToken: token,
            RefreshToken: refreshToken,
            UserId: user.Id,
            Email: user.Email!,
            UserName: user.UserName!
        );

        return Result<LoginResponse>.Success(response);
    }
}

public record LoginResponse(
    string AccessToken,
    string RefreshToken,
    string UserId,
    string Email,
    string UserName
);

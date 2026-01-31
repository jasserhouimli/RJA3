using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RJA3.Modules.Auth;
using RJA3.Modules.Auth.Domain;
using RJA3.Modules.Auth.Persistence;
using RJA3.Modules.Auth.Services;
using RJA3.Shared;


namespace RJA3.Modules.Auth.Features.RefreshToken;

public class RefreshTokenHandler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenService _jwtTokenService;
    private readonly IRefreshTokenService _refreshTokenService;
    private readonly IConfiguration _configuration;

    public RefreshTokenHandler(
        ApplicationDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        IJwtTokenService jwtTokenService,
        IRefreshTokenService refreshTokenService,
        IConfiguration configuration)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _jwtTokenService = jwtTokenService;
        _refreshTokenService = refreshTokenService;
        _configuration = configuration;
    }

    public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenCommand command)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == command.RefreshToken);

        if (refreshToken == null || !refreshToken.IsActive)
        {
            return Result<RefreshTokenResponse>.Failure("Invalid or expired refresh token.");
        }

        var user = refreshToken.User;

        var newAccessToken = _jwtTokenService.GenerateToken(user);
        var newRefreshToken = _refreshTokenService.GenerateRefreshToken();
        var refreshTokenExpiryDays = int.Parse(_configuration["Jwt:RefreshTokenExpiryDays"] ?? "7");

        refreshToken.IsRevoked = true;
        refreshToken.ReplacedByToken = newRefreshToken;

        var generatedRefreshToken = new Domain.RefreshToken
        {
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenExpiryDays)
        };

        _dbContext.RefreshTokens.Add(generatedRefreshToken);
        await _dbContext.SaveChangesAsync();

        var response = new RefreshTokenResponse(
            AccessToken: newAccessToken,
            RefreshToken: newRefreshToken
        );

        return Result<RefreshTokenResponse>.Success(response);
    }
}

public record RefreshTokenResponse(
    string AccessToken,
    string RefreshToken
);

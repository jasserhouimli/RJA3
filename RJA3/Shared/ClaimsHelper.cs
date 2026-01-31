using System.Security.Claims;

namespace RJA3.Shared;

public static class ClaimsHelper
{
    public static string? GetUserId(this ClaimsPrincipal userClaims)
    {
        return userClaims.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public static string? GetEmail(this ClaimsPrincipal userClaims)
    {
        return userClaims.FindFirst(ClaimTypes.Email)?.Value;
    }

    public static string? GetUserName(this ClaimsPrincipal userClaims)
    {
        return userClaims.FindFirst(ClaimTypes.Name)?.Value;
    }
}

using Microsoft.AspNetCore.Mvc;

namespace RJA3.Modules.Auth.Features.RefreshToken;

public static class RefreshTokenEndpoint
{
    public static IEndpointRouteBuilder MapRefreshTokenEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/refresh-token", async ([FromBody] RefreshTokenCommand command, RefreshTokenHandler handler) =>
        {
            var result = await handler.Handle(command);

            if (result.IsSuccess)
            {
                return Results.Ok(result.Data);
            }

            return Results.Unauthorized();
        })
        .WithName("RefreshToken")
        .RequireAuthorization();


        return app;
    }
}

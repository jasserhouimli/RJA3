using Microsoft.AspNetCore.Mvc;

namespace RJA3.Modules.Auth.Features.Login;

public static class LoginEndpoint
{
    public static IEndpointRouteBuilder MapLoginEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/login", async ([FromBody] LoginCommand command, LoginHandler handler) =>
        {
            var result = await handler.Handle(command);

            if (result.IsSuccess)
            {
                return Results.Ok(result.Data);
            }

            return Results.Unauthorized();
        })
        .WithName("Login");


        return app;
    }
}

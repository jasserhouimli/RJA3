using Microsoft.AspNetCore.Mvc;

namespace RJA3.Modules.Auth.Features.Register;

public static class RegisterEndpoint
{
    public static IEndpointRouteBuilder MapRegisterEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/auth/register", async ([FromBody] RegisterCommand command, RegisterHandler handler) =>
        {
            var result = await handler.Handle(command);

            if (result.IsSuccess)
            {
                return Results.Created($"/auth/users/{result.Data!.UserId}", result.Data);
            }

            return Results.BadRequest(new { error = result.Error });
        })
        .WithName("Register");

        return app;
    }
}
using System.Security.Claims;

public static class GetMeEndpoint
{



    public static IEndpointRouteBuilder MapGetMeEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users/me", async (GetMeHandler getMeHandler, ClaimsPrincipal userClaims) =>
        {
            var result = await getMeHandler.Handle(userClaims);
            if(result.IsSuccess)
            {
                return Results.Ok(result.Data);
            }
            else
            {
                return Results.BadRequest(result.Error);
            }
        })
        .WithTags("Users")
        .WithName("GetMe")
        .WithSummary("Gets the current authenticated user's information.")
        .RequireAuthorization();

        return app;
    }
}
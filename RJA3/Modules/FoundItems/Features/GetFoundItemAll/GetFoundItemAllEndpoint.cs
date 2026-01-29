using Microsoft.AspNetCore.Mvc;

namespace RJA3.Modules.FoundItems.Features.GetFoundItemAll
{
    public static class GetFoundItemAllEndpoint
    {
        public static IEndpointRouteBuilder MapGetFoundItemAllEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/founditems", async ([AsParameters] GetFoundItemAllQuery query, GetFoundItemAllHandler handler) =>
            {
                var result = await handler.Handle(query);

                if (result.IsSuccess)
                {
                    return Results.Ok(result.Data);
                }
                else
                {
                    return Results.BadRequest(result.Error);
                }
            });

            return app;
        }
    }
}
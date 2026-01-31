using Microsoft.AspNetCore.Mvc;

namespace RJA3.Modules.Items.FoundItems.Features.GetFoundItemById
{
    public static class GetFoundItemByIdEndpoint
    {
        public static IEndpointRouteBuilder MapGetFoundItemByIdEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/founditems/{foundItemId}", async (string foundItemId, GetFoundItemByIdHandler handler) =>
            {
                var query = new GetFoundItemByIdQuery { FoundItemId = foundItemId };
                var result = await handler.Handle(query);

                if (result.IsSuccess)
                {
                    return Results.Ok(result.Data);
                }
                else
                {
                    return Results.NotFound(result.Error);
                }
            })
            .RequireAuthorization();

            return app;
        }
    }
}
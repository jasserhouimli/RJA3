

using Microsoft.AspNetCore.Mvc;
using RJA3.Modules.LostItems.Features.GetReportLostItemById;

public static class GetReportLostItemByIdEndpoint
{
    public static IEndpointRouteBuilder MapGetReportLostItemByIdEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/lostitems/{lostItemId}", async ([FromRoute] string lostItemId, GetReportLostItemByIdHandler handler) =>
        {
            var query = new GetReportLostItemByIdQuery(lostItemId);
            var result = await handler.Handle(query);

            if(result.IsSuccess == false)
            {
                return Results.BadRequest(result.Error);
            }

            if (result.Data == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(result.Data);
        });

        return app;
    }
}
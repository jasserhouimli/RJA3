using Microsoft.AspNetCore.Mvc;
using RJA3.Shared;

public static class GetReportLostItemAllEndpoint
{
    public static IEndpointRouteBuilder MapGetReportLostItemAllEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/lostitems", async (
            GetReportLostItemAllHandler handler,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10) =>
        {
            var query = new GetReportLostItemAllQuery(pageNumber, pageSize);
            var result = await handler.Handle(query);
            
            if (result.IsSuccess && result.Data != null)
            {
                return Results.Ok(new
                {
                    Items = result.Data.Items,
                    TotalCount = result.Data.TotalCount,
                    PageNumber = result.Data.PageNumber,
                    PageSize = result.Data.PageSize,
                    TotalPages = result.Data.TotalPages
                });
            }
            else
            {
                return Results.BadRequest(result.Error);
            }
        });

        return app;
    }
}
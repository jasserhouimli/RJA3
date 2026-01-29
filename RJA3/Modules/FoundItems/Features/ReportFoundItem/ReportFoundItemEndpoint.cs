using Microsoft.AspNetCore.Mvc;
using RJA3.Modules.FoundItems.Persistence;

namespace RJA3.Modules.FoundItems.Features.ReportFoundItem
{
    public static class ReportFoundItemEndpoint
    {
        public static IEndpointRouteBuilder MapReportFoundItemEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/founditems/add", async ([FromBody] ReportFoundItemCommand cmd, ReportFoundItemHandler handler) =>
            {
                var result = await handler.Handle(cmd);

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
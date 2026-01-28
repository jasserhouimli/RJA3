using Microsoft.AspNetCore.Mvc;

namespace RJA3.Modules.LostAndFound.Features.ReportLostItem
{
    public static class ReportLostItemEndpoint
    {


        public static IEndpointRouteBuilder MapReportLostItemEndpoint(this IEndpointRouteBuilder app)
        {


            app.MapPost("/lostitems/create", async ([FromBody] ReportLostItemCommand cmd, ReportLostItemHandler handler) =>
            {
                var result = await handler.Handle(cmd);

                return Results.Ok(result);
            });

            return app;
        }
        
    }
}

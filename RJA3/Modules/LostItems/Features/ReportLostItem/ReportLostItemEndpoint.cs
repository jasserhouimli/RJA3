using Microsoft.AspNetCore.Mvc;
using RJA3.Modules.LostItems.Persistence;

namespace RJA3.Modules.LostItems.Features.ReportLostItem
{
    public static class ReportLostItemEndpoint
    {

        
        public static IEndpointRouteBuilder MapReportLostItemEndpoint(this IEndpointRouteBuilder app)
        {
                      


            app.MapPost("/lostitems/add", async ([FromBody] ReportLostItemCommand cmd, ReportLostItemHandler handler) =>
            {
                var result = await handler.Handle(cmd);

                return Results.Ok(result);
            });


            return app;
        }
        
    }
}

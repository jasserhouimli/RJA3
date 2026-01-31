using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RJA3.Shared;

namespace RJA3.Modules.Items.LostItems.Features.ReportLostItem
{
    public static class ReportLostItemEndpoint
    {

        
        public static IEndpointRouteBuilder MapReportLostItemEndpoint(this IEndpointRouteBuilder app)
        {
                      


            app.MapPost("/lostitems/add", async ([FromBody] ReportLostItemCommand cmd, ReportLostItemHandler handler, ClaimsPrincipal userClaims) =>
            {
                var userId = userClaims.GetUserId();
                if (userId == null)
                {
                    return Results.Unauthorized();
                }

                var result = await handler.Handle(cmd, userId);

                if (result.IsSuccess)
                {
                    return Results.Ok(result.Data);
                }
                else
                {
                    return Results.BadRequest(result.Error);
                }
            })
            .RequireAuthorization();


            return app;
        }
        
    }
}
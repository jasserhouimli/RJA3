using Microsoft.AspNetCore.Mvc;
using RJA3.Shared;

namespace RJA3.Modules.Items.FoundItems.Features.ReportFoundItem
{
    public static class ReportFoundItemEndpoint
    {
        public static IEndpointRouteBuilder MapReportFoundItemEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapPost("/founditems/add", async ([FromBody] ReportFoundItemCommand cmd, ReportFoundItemHandler handler, HttpContext httpContext) =>
            {
                var userId = httpContext.User.GetUserId();
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
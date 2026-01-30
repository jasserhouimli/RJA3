

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace RJA3.Modules.ItemsMatcher.Features.GetMatchesByLostItem;

public static class GetMatchesByLostItemEndpoint
{
    

    public static void MapGetMatchesByLostItemEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/items-matcher/lost-items/{lostItemId}/matches", async (string lostItemId, GetMatchesByLostItemHandler handler) =>
        {
            var query = new GetMatchesByLostItemQuery(lostItemId);
            var matches = await handler.Handle(query);
            return Results.Ok(matches);
        })
        .WithTags("ItemsMatcher")
        .WithName("GetMatchesByLostItem")
        .WithDescription("Get matches for a lost item by its ID.");
    }
}
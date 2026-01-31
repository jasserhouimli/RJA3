
namespace RJA3.Modules.Items.ItemsMatcher.Features.GetMatchesByLostItem;

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
        .RequireAuthorization();
    }
}
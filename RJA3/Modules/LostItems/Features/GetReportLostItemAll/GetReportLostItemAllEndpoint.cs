public static class GetReportLostItemAllEndpoint
{
    public static IEndpointRouteBuilder MapGetReportLostItemAllEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/lostitems", async (GetReportLostItemAllHandler handler) =>
        {
            var query = new GetReportLostItemAllQuery();
            var result = await handler.Handle(query);
            return Results.Ok(result);
        });
        

        return app;
    }
}
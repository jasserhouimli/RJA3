namespace RJA3.Modules.Items.FoundItems.Features.GetSecurityQuestionsByFoundItemId
{
    public static class GetSecurityQuestionsByFoundItemIdEndpoint
    {
        public static IEndpointRouteBuilder MapGetSecurityQuestionsByFoundItemIdEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/founditems/{foundItemId}/securityquestions", async (string foundItemId, GetSecurityQuestionsByFoundItemIdHandler handler) =>
            {
                var query = new GetSecurityQuestionsByFoundItemIdQuery { FoundItemId = foundItemId };
                var result = await handler.Handle(query);

                if (result.IsSuccess)
                {
                    return Results.Ok(result.Data);
                }
                else
                {
                    return Results.NotFound(result.Error);
                }
            })
            .RequireAuthorization();

            return app;
        }
    }
}
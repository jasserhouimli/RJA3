using RJA3.Modules.LostAndFound.Features.LostItems.Domain;
using RJA3.Modules.LostAndFound.Features.LostItems.Persistence;
using RJA3.Modules.LostAndFound.Features.LostItems.ReportLostItem;

namespace RJA3.Modules.LostAndFound
{
    public static class LostAndFoundItemModule
    {

        public static IServiceCollection AddLostItemServices(this IServiceCollection services)
        {
            services.AddScoped<ReportLostItemHandler>();

            services.AddScoped<ILostItemRepository, LostItemRepository>();
            return services;
        }


        public static IEndpointRouteBuilder MapLostItemEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapReportLostItemEndpoint();
            return app;
        }
    }
}

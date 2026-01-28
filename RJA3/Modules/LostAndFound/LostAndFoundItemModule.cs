using RJA3.Modules.LostAndFound.Domain;
using RJA3.Modules.LostAndFound.Features.LostItems.ReportLostItem;
using RJA3.Modules.LostAndFound.Persistence;

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

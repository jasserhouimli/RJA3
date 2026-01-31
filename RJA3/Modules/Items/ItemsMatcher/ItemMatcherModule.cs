using RJA3.Modules.Items.ItemsMatcher.Domain;
using RJA3.Modules.Items.ItemsMatcher.Features.GetMatchesByLostItem;
using FluentValidation;

namespace RJA3.Modules.Items.ItemsMatcher
{
    public static class ItemMatcherModule
    {
        public static IServiceCollection AddItemMatcherServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<GetMatchesByLostItemQuery>();
            services.AddScoped<GetMatchesByLostItemHandler>();
            services.AddScoped<MatchScoreCalculator>();
            services.AddScoped<IItemsMatcherRepository, ItemsMatcherRepository>();
            return services;
        }

        public static IEndpointRouteBuilder MapItemMatcherEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGetMatchesByLostItemEndpoint();
            return app;
        }
    }
}

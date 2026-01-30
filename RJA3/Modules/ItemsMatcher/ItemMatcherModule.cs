using Microsoft.Extensions.DependencyInjection;
using RJA3.Modules.ItemsMatcher.Domain;
using RJA3.Modules.ItemsMatcher.Features.GetMatchesByLostItem;
using RJA3.Modules.ItemsMatcher.Persistence;
using FluentValidation;

namespace RJA3.Modules.ItemsMatcher
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

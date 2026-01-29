using Microsoft.EntityFrameworkCore;
using RJA3.Modules.LostItems.Domain;
using RJA3.Modules.LostItems.Features.ReportLostItem;
using RJA3.Modules.LostItems.Features.GetReportLostItemById;
using RJA3.Modules.LostItems.Persistence;
using System.Runtime.CompilerServices;

namespace RJA3.Modules.LostItems
{
    public static class LostItemModule
    {

        public static IServiceCollection AddLostItemServices(this IServiceCollection services , IConfiguration conf)
        {
            services.AddScoped<ReportLostItemHandler>();
            services.AddScoped<GetReportLostItemByIdHandler>();
            services.AddScoped<GetReportLostItemAllHandler>();
            services.AddDbContext<LostItemDbContext>(options =>
            {
                options.UseNpgsql(conf.GetConnectionString("PostgreSQL"));
            });

            services.AddScoped<ILostItemRepository, LostItemRepository>();
            return services;
        }


        public static IEndpointRouteBuilder MapLostItemEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapReportLostItemEndpoint();
            app.MapGetReportLostItemByIdEndpoint();
            app.MapGetReportLostItemAllEndpoint();
            return app;
        }
    }
}

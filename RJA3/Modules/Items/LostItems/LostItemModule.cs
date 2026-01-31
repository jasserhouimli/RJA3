using Microsoft.EntityFrameworkCore;
using RJA3.Modules.Items.LostItems.Domain;
using RJA3.Modules.Items.LostItems.Persistence;
using RJA3.Modules.Items.LostItems.Features.ReportLostItem;
using RJA3.Modules.Items.LostItems.Features.GetReportLostItemById;
using RJA3.Modules.Items.LostItems.Features.GetReportLostItemAll;
using FluentValidation;

namespace RJA3.Modules.Items.LostItems
{   
    public static class LostItemModule
    {

        public static IServiceCollection AddLostItemServices(this IServiceCollection services , IConfiguration conf)
        {
            services.AddValidatorsFromAssemblyContaining<GetReportLostItemAllQuery>();
            services.AddValidatorsFromAssemblyContaining<ReportLostItemCommand>();
            services.AddValidatorsFromAssemblyContaining<GetReportLostItemByIdQuery>();
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

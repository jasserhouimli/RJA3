using Microsoft.EntityFrameworkCore;
using RJA3.Modules.Items.FoundItems.Domain;
using RJA3.Modules.Items.FoundItems.Persistence;
using RJA3.Modules.Items.FoundItems.Features.ReportFoundItem;
using RJA3.Modules.Items.FoundItems.Features.GetFoundItemById;
using RJA3.Modules.Items.FoundItems.Features.GetFoundItemAll;
using RJA3.Modules.Items.FoundItems.Features.GetSecurityQuestionsByFoundItemId;
using FluentValidation;

namespace RJA3.Modules.Items.FoundItems
{
    public static class FoundItemModule
    {

        public static IServiceCollection AddFoundItemServices(this IServiceCollection services , IConfiguration conf)
        {
            services.AddValidatorsFromAssemblyContaining<GetFoundItemAllQuery>();
            services.AddValidatorsFromAssemblyContaining<ReportFoundItemCommand>();
            services.AddValidatorsFromAssemblyContaining<GetFoundItemByIdQuery>();
            services.AddValidatorsFromAssemblyContaining<GetSecurityQuestionsByFoundItemIdQuery>();
            services.AddScoped<ReportFoundItemHandler>();
            services.AddScoped<GetFoundItemByIdHandler>();
            services.AddScoped<GetFoundItemAllHandler>();
            services.AddScoped<GetSecurityQuestionsByFoundItemIdHandler>();
            services.AddDbContext<FoundItemDbContext>(options =>
            {
                options.UseNpgsql(conf.GetConnectionString("PostgreSQL"));
            });

            services.AddScoped<IFoundItemRepository, FoundItemRepository>();
            return services;
        }


        public static IEndpointRouteBuilder MapFoundItemEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapReportFoundItemEndpoint();
            app.MapGetFoundItemByIdEndpoint();
            app.MapGetFoundItemAllEndpoint();
            app.MapGetSecurityQuestionsByFoundItemIdEndpoint();
            return app;
        }
    }
}
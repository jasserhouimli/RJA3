using Microsoft.EntityFrameworkCore;
using RJA3.Modules.FoundItems.Domain;
using RJA3.Modules.FoundItems.Features.ReportFoundItem;
using RJA3.Modules.FoundItems.Features.GetFoundItemById;
using RJA3.Modules.FoundItems.Persistence;
using System.Runtime.CompilerServices;
using FluentValidation;
using RJA3.Modules.FoundItems.Features.GetFoundItemAll;
using RJA3.Modules.FoundItems.Features.GetSecurityQuestionsByFoundItemId;

namespace RJA3.Modules.FoundItems
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
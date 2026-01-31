using Microsoft.EntityFrameworkCore;
using RJA3.Modules.Auth.Events;
using RJA3.Modules.Users.EventHandlers;
using RJA3.Modules.Users.Persistence;
using RJA3.Shared.Events;

namespace RJA3.Modules.Users;

public static class UserModule
{
    public static IServiceCollection AddUserServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));

        services.AddScoped<UserRegisteredEventHandler>();
        services.AddScoped<IEventHandler<UserRegisteredEvent>>(sp => sp.GetRequiredService<UserRegisteredEventHandler>());
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<GetMeHandler>();


        return services;
    }

    public static void SubscribeToEvents(IEventBus eventBus)
    {
        eventBus.Subscribe<UserRegisteredEvent, UserRegisteredEventHandler>();
    }

    public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder app)


    {
        app.MapGetMeEndpoint();
        return app;
    }
}

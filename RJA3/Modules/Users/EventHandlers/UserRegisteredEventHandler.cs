using RJA3.Modules.Auth.Events;
using RJA3.Modules.Users.Domain;
using RJA3.Modules.Users.Persistence;
using RJA3.Shared.Events;

namespace RJA3.Modules.Users.EventHandlers;

public class UserRegisteredEventHandler : IEventHandler<UserRegisteredEvent>
{
    private readonly UserDbContext _dbContext;
    private readonly ILogger<UserRegisteredEventHandler> _logger;

    public UserRegisteredEventHandler(UserDbContext dbContext, ILogger<UserRegisteredEventHandler> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task HandleAsync(UserRegisteredEvent @event)
    {
        _logger.LogInformation("Handling UserRegisteredEvent for user {UserId}", @event.UserId);

        var userProfile = new UserProfile
        {
            UserId = @event.UserId,
            UserName = @event.UserName,
            Email = @event.Email,
            CreatedAt = @event.RegisteredAt,
            IsActive = true
        };

        _dbContext.UserProfiles.Add(userProfile);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("User profile created for user {UserId}", @event.UserId);
    }
}

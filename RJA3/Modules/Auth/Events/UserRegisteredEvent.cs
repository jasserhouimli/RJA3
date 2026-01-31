using RJA3.Shared.Events;

namespace RJA3.Modules.Auth.Events;

public class UserRegisteredEvent : IEvent
{
    public string UserId { get; init; } = null!;
    public string UserName { get; init; } = null!;
    public string Email { get; init; } = null!;
    public DateTime RegisteredAt { get; init; }
}

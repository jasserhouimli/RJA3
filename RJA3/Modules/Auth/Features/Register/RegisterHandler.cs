using FluentValidation;
using Microsoft.AspNetCore.Identity;
using RJA3.Modules.Auth.Domain;
using RJA3.Modules.Auth.Events;
using RJA3.Shared;
using RJA3.Shared.Events;

namespace RJA3.Modules.Auth.Features.Register;

public class RegisterHandler
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IValidator<RegisterCommand> _validator;
    private readonly IEventBus _eventBus;

    public RegisterHandler(UserManager<ApplicationUser> userManager, IEventBus eventBus)
    {
        _userManager = userManager;
        _validator = new RegisterValidator(userManager);
        _eventBus = eventBus;
    }

    public async Task<Result<RegisterResponse>> Handle(RegisterCommand command)
    {
        var validationResult = await _validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
            return Result<RegisterResponse>.Failure(errors);
        }

        var user = new ApplicationUser
        {
            UserName = command.UserName,
            Email = command.Email,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, command.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<RegisterResponse>.Failure(errors);
        }

        await _eventBus.PublishAsync(new UserRegisteredEvent
        {
            UserId = user.Id,
            UserName = user.UserName!,
            Email = user.Email!,
            RegisteredAt = DateTime.UtcNow
        });

        var response = new RegisterResponse(user.Id, user.UserName!, user.Email!);
        return Result<RegisterResponse>.Success(response);
    }
}

public record RegisterResponse(
    string UserId,
    string UserName,
    string Email
);
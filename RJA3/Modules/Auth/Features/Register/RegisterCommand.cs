namespace RJA3.Modules.Auth.Features.Register;

public record RegisterCommand(
    string UserName,
    string Email,
    string Password
);
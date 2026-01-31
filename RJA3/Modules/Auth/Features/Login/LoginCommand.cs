namespace RJA3.Modules.Auth.Features.Login;

public record LoginCommand(
    string Email,
    string Password
);

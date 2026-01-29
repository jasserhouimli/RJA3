using FluentValidation;
using RJA3.Modules.FoundItems.Domain;
using RJA3.Modules.FoundItems.Features.ReportFoundItem;
using RJA3.Modules.FoundItems.Persistence;
using RJA3.Shared;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RJA3.Modules.FoundItems.Features.ReportFoundItem;

public sealed class ReportFoundItemHandler(IFoundItemRepository foundItemRep, IValidator<ReportFoundItemCommand> validator, IHttpContextAccessor httpContextAccessor)
{
    
    public async Task<Result<ReportFoundItemResult>> Handle(ReportFoundItemCommand command)
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // if (string.IsNullOrEmpty(userId))
        // {
        //     return Result<ReportFoundItemResult>.Failure("User not authenticated");
        // }

        var validationResult = await validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return Result<ReportFoundItemResult>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        FoundItem item = command.ItemType switch
        {
            FoundItemType.Phone => new PhoneFoundItem(
                userId ?? Guid.NewGuid().ToString(),
                DateTime.UtcNow,
                command.LocationFound,
                command.Brand!,
                command.Model!,
                command.Color!,
                command.SecurityQuestions
            ),
            _ => throw new NotImplementedException($"Item type {command.ItemType} is not supported yet.")
        };

        await foundItemRep.AddAsync(item);

        return Result<ReportFoundItemResult>.Success(new ReportFoundItemResult
        {
            FoundItemId = item.Id
        });

    }

}

public sealed class ReportFoundItemResult
{
    public string FoundItemId { get; init; }
}
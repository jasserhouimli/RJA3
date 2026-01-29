using FluentValidation;
using RJA3.Modules.LostAndFound.Domain;
using RJA3.Modules.LostItems.Domain;
using RJA3.Modules.LostItems.Features.ReportLostItem;
using RJA3.Modules.LostItems.Persistence;
using RJA3.Shared;

namespace RJA3.Modules.LostItems.Features.ReportLostItem;

public sealed class ReportLostItemHandler(ILostItemRepository _lostItemRep , IValidator<ReportLostItemCommand> _validator)
{
    
    public async Task<Result<ReportLostItemResult>> Handle(ReportLostItemCommand command)
    {


        var validationResult = await _validator.ValidateAsync(command);
        if (!validationResult.IsValid)
        {
            return Result<ReportLostItemResult>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }


        LostItem item = command.ItemType switch
        {
            LostItemType.Phone => new PhoneLostItem(
                Guid.NewGuid().ToString(), //// USERID FOR TESTING UNTIL I IMPLEMENT AUTH
                DateTime.UtcNow, /// DATE JUST NOW FOR TESTING
                command.LocationLost,
                command.Brand!,
                command.Model!,
                command.Color!
            ),
            _ => throw new NotImplementedException($"Item type {command.ItemType} is not supported yet.")
        };

        await _lostItemRep.AddAsync(item);

        return Result<ReportLostItemResult>.Success(new ReportLostItemResult
        {
            LostItemId = item.Id
        });

    }

}


public sealed class ReportLostItemResult
{
    public string LostItemId { get; init; }

}
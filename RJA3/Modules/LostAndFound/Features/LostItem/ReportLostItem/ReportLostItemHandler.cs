using RJA3.Modules.LostAndFound.Domain;
using RJA3.Modules.LostAndFound.Features.LostItems.Domain;

namespace RJA3.Modules.LostAndFound.Features.LostItems.ReportLostItem;

public sealed class ReportLostItemHandler
{
    private readonly ILostItemRepository _repository;

    public ReportLostItemHandler(ILostItemRepository repository)
    {
        _repository = repository;
    }

    public async Task<ReportLostItemResult> Handle(ReportLostItemCommand command)
    {


        LostItem item = command.ItemType switch
        {
            LostItemType.Phone => new PhoneLostItem(
                command.UserId,
                command.DateLost,
                command.LocationLost,
                command.Brand!,
                command.Model!,
                command.Color!),
        };

        await _repository.AddAsync(item);

        return new ReportLostItemResult
        {
            LostItemId = item.Id
        };

    }
}

public sealed class ReportLostItemResult
{
    public Guid LostItemId { get; init; }



}


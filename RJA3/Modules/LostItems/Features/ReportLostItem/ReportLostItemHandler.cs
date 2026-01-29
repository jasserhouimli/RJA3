using RJA3.Modules.LostAndFound.Domain;
using RJA3.Modules.LostAndFound.Features.ReportLostItem;
using RJA3.Modules.LostAndFound.Persistence;

namespace RJA3.Modules.LostAndFound.Features.ReportLostItem;

public sealed class ReportLostItemHandler(ILostItemRepository lostItemRep)
{
    
    public async Task<ReportLostItemResult> Handle(ReportLostItemCommand command)
    {


        LostItem item = command.ItemType switch
        {
            LostItemType.Phone => new PhoneLostItem(
                Guid.NewGuid().ToString(), //// USERID FOR TESTING
                DateTime.UtcNow, /// DATE JUST NOW FOR TESTING
                command.LocationLost,
                command.Brand!,
                command.Model!,
                command.Color!
            ),
        };

        await lostItemRep.AddAsync(item);

        return new ReportLostItemResult
        {
            LostItemId = item.Id
        };

    }

    public async Task<List<LostItem>> Handle()
    {
        var result = await lostItemRep.GetAllLostItemsAsync();

        return result;
    }
}


public sealed class ReportLostItemResult
{
    public string LostItemId { get; init; }

}


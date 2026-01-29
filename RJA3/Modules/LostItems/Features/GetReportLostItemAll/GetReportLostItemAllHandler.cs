using RJA3.Modules.LostItems.Domain;

public sealed class GetReportLostItemAllHandler(ILostItemRepository _lostItemRepository)
{
    public async Task<List<LostItem>> Handle(GetReportLostItemAllQuery query)
    {
        var result = await _lostItemRepository.GetAllLostItemsAsync();
        return result;
    }

}
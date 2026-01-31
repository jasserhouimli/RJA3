using RJA3.Modules.Items.ItemsMatcher.Domain;

namespace RJA3.Modules.Items.ItemsMatcher.Features.GetMatchesByLostItem;

public class GetMatchesByLostItemHandler
{
    private readonly IItemsMatcherRepository _itemsMatcherRepository;

    public GetMatchesByLostItemHandler(IItemsMatcherRepository itemsMatcherRepository)
    {
        _itemsMatcherRepository = itemsMatcherRepository;
    }

    public async Task<List<MatchResult>> Handle(GetMatchesByLostItemQuery query)
    {
        var matches = await _itemsMatcherRepository.FindMatchesAsyncByLostItem(query.LostItemId);
        return matches;
    }
}
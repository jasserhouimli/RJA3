namespace RJA3.Modules.Items.ItemsMatcher.Domain;

public interface IItemsMatcherRepository
{
    
    Task<List<MatchResult>> FindMatchesAsyncByLostItem(string lostItemId);
    Task<List<MatchResult>> FindMatchesAsyncByFoundItem(string foundItemId);
}


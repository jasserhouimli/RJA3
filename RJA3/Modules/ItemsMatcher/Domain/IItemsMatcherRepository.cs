using System.Collections.Generic;
using System.Threading.Tasks;

namespace RJA3.Modules.ItemsMatcher.Domain;

public interface IItemsMatcherRepository
{
    
    Task<List<MatchResult>> FindMatchesAsyncByLostItem(string lostItemId);
    Task<List<MatchResult>> FindMatchesAsyncByFoundItem(string foundItemId);
}


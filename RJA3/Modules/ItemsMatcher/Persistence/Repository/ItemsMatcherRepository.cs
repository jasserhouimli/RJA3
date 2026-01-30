

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RJA3.Modules.FoundItems.Domain;
using RJA3.Modules.LostItems.Domain;
using RJA3.Modules.ItemsMatcher.Domain;

namespace RJA3.Modules.ItemsMatcher.Persistence;

public class ItemsMatcherRepository : IItemsMatcherRepository
{

    private readonly IFoundItemRepository _foundItemRepository;
    private readonly ILostItemRepository _lostItemRepository;
    private readonly MatchScoreCalculator _matchScoreCalculator;

    public ItemsMatcherRepository(IFoundItemRepository foundItemRepository, ILostItemRepository lostItemRepository, MatchScoreCalculator matchScoreCalculator)
    {
        _foundItemRepository = foundItemRepository;
        _lostItemRepository = lostItemRepository;
        _matchScoreCalculator = matchScoreCalculator;
    }

    public async Task<List<MatchResult>> FindMatchesAsyncByLostItem(string lostItemId)
    {

        LostItem? lostItem = await _lostItemRepository.GetLostItemByIdAsync(lostItemId);

        if (lostItem == null)
        {
            throw new Exception("Lost item not found");
        }

        List<FoundItem> foundItems = await _foundItemRepository.GetFoundItemsByTypeAsync(lostItem.ItemType switch
        {
            LostItemType.Phone => FoundItemType.Phone,
            LostItemType.Document => FoundItemType.Document,
        });

        List<MatchResult> matches = new List<MatchResult>();

        foreach (var foundItem in foundItems)
        {
            matches.Add(new MatchResult
            {
                FoundItemId = foundItem.Id,
                LostItemId = lostItem.Id,
                MatchScore = _matchScoreCalculator.CalculateMatchScore(lostItem, foundItem).MatchScore,
                DistanceInMeters = _matchScoreCalculator.CalculateDistanceFromLongitudeLatitude(
                    lostItem.Latitude,
                    lostItem.Longitude,
                    foundItem.Latitude,
                    foundItem.Longitude
                )
            });
            
        }

        var sortedMatches = matches.OrderByDescending(m => m.MatchScore).ThenBy(m => m.DistanceInMeters).ToList();

        return sortedMatches;


        
    }

    public async Task<List<MatchResult>> FindMatchesAsyncByFoundItem(string foundItemId)
    {
        throw new NotImplementedException();
    }
}
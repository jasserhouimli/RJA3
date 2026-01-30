

using RJA3.Modules.FoundItems.Domain;
using RJA3.Modules.LostItems.Domain;

namespace RJA3.Modules.ItemsMatcher.Domain;

public class MatchScoreCalculator
{

    private readonly ILogger<MatchScoreCalculator> _logger;

    public MatchScoreCalculator(ILogger<MatchScoreCalculator> logger)
    {
        _logger = logger;
    }

    public MatchResult CalculateMatchScore(LostItem lostItem, FoundItem foundItem)
    {
        _logger.LogInformation("Starting match score calculation for lost item {LostItemId} (type: {LostItemType}) and found item {FoundItemId} (type: {FoundItemType})", 
            lostItem.Id, lostItem.ItemType, foundItem.Id, foundItem.ItemType);

        int score = 0;

        if(lostItem.ItemType.ToString() != foundItem.ItemType.ToString())
        {
            _logger.LogInformation("Item types do not match: {LostType} vs {FoundType}, returning score 0", lostItem.ItemType, foundItem.ItemType);
            return new MatchResult { MatchScore = 0 };
        }

        switch (lostItem)
        {
            case PhoneLostItem lostPhone when foundItem is PhoneFoundItem foundPhone:
                double distance = CalculateDistanceFromLongitudeLatitude(lostPhone.Latitude, lostPhone.Longitude, foundPhone.Latitude, foundPhone.Longitude);
                _logger.LogInformation("Calculating match score for phone lost item {LostPhoneId} and found item {FoundPhoneId} with distance {Distance} meters", lostPhone.Id, foundPhone.Id, distance);
                
                if (lostPhone.Brand.Equals(foundPhone.Brand, StringComparison.OrdinalIgnoreCase))
                {
                    score += 20;
                    _logger.LogInformation("Brand match: {Brand}, score increased to {Score}", lostPhone.Brand, score);
                }
                else
                {
                    _logger.LogInformation("Brand mismatch: lost {LostBrand} vs found {FoundBrand}", lostPhone.Brand, foundPhone.Brand);
                }
                // if (lostPhone.Model.Equals(foundPhone.Model, StringComparison.OrdinalIgnoreCase))
                // {
                //     score += 20;
                // } // i will ignore model for now as many people dont know the model of their lost phone
                // if (lostPhone.Color.Equals(foundPhone.Color, StringComparison.OrdinalIgnoreCase))
                // {
                //     score += 10;
                // } i will think about it later because color can be relative and people can describe colors differently so mayble i will add approximate color matching by rgb values later
                _logger.LogInformation("Final score for phones: {Score}", score);
                return new MatchResult { MatchScore = score, DistanceInMeters = distance };


            // Add cases for other item types here

            default:
                _logger.LogInformation("Unknown item type {Type}, returning score 0", lostItem.ItemType);
                return new MatchResult { MatchScore = 0 }; // 0 score if item types do not match known types
        }


        
    }

    
    /// i just found this formula on internet
    public double CalculateDistanceFromLongitudeLatitude(double lat1, double lon1, double lat2, double lon2)
    {
        var R = 6371e3; 
        var φ1 = lat1 * Math.PI / 180;
        var φ2 = lat2 * Math.PI / 180;
        var Δφ = (lat2 - lat1) * Math.PI / 180;
        var Δλ = (lon2 - lon1) * Math.PI / 180;

        var a = Math.Sin(Δφ / 2) * Math.Sin(Δφ / 2) +
                Math.Cos(φ1) * Math.Cos(φ2) *
                Math.Sin(Δλ / 2) * Math.Sin(Δλ / 2);
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        var d = R * c;

        
        return d; 
    }
}


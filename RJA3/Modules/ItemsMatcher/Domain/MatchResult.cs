namespace RJA3.Modules.ItemsMatcher.Domain;

public class MatchResult
{
    public string LostItemId { get; set; }
    public string FoundItemId { get; set; }
    public double MatchScore { get; set; }

    public double DistanceInMeters { get; set; }
}
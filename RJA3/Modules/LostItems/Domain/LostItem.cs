namespace RJA3.Modules.LostItems.Domain;

public abstract class LostItem
{
    public string Id { get; private set; }
    public string OwnerId { get; private set; }
    public DateTime LostAt { get; private set; }
    public double Latitude { get; protected set; }
    public double Longitude { get; protected set; } 
    public LostItemStatus Status { get; protected set; }

    public LostItemType ItemType { get; protected set; }


    // public string LostItemImageUrl { get; set; } = default!; // it will be useful later when Q/A system is implemented

    protected LostItem() { } 

    protected LostItem(
        string ownerId,
        DateTime lostAt,
        double latitude, double longitude)
    {
        Id = Guid.NewGuid().ToString();
        OwnerId = ownerId;
        LostAt = lostAt;
        Latitude = latitude;
        Longitude = longitude;
        Status = LostItemStatus.ReportedLost;
    }
}

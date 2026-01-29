namespace RJA3.Modules.LostAndFound.Domain;

public abstract class LostItem
{
    public string Id { get; private set; }
    public string OwnerId { get; private set; }
    public DateTime LostAt { get; private set; }
    public string Location { get; private set; } = default!;
    public LostItemStatus Status { get; protected set; }

    public LostItemType ItemType { get; protected set; }

    protected LostItem() { } 

    protected LostItem(
        string ownerId,
        DateTime lostAt,
        string location)
    {
        if (ownerId == string.Empty)
            throw new ArgumentException("OwnerId is required");

        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location is required");

        Id = Guid.NewGuid().ToString();
        OwnerId = ownerId;
        LostAt = lostAt;
        Location = location;
        Status = LostItemStatus.ReportedLost;
    }
}

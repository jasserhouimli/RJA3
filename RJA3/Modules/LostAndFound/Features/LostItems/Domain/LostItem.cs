namespace RJA3.Modules.LostAndFound.Features.LostItems.Domain;

public abstract class LostItem
{
    public Guid Id { get; private set; }
    public Guid OwnerId { get; private set; }
    public DateTime LostAt { get; private set; }
    public string Location { get; private set; } = default!;
    public LostItemStatus Status { get; protected set; }

    public LostItemType ItemType { get; protected set; }

    protected LostItem() { } 

    protected LostItem(
        Guid ownerId,
        DateTime lostAt,
        string location)
    {
        if (ownerId == Guid.Empty)
            throw new ArgumentException("OwnerId is required");

        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location is required");

        Id = Guid.NewGuid();
        OwnerId = ownerId;
        LostAt = lostAt;
        Location = location;
        Status = LostItemStatus.Reported;
    }
}

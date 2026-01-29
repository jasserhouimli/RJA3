namespace RJA3.Modules.FoundItems.Domain;

public abstract class FoundItem
{
    public string Id { get; private set; }
    public string FinderId { get; private set; }
    public DateTime FoundAt { get; private set; }
    public string Location { get; private set; } = default!;
    public FoundItemStatus Status { get; protected set; }

    public FoundItemType ItemType { get; protected set; }

    public List<SecurityQuestion> SecurityQuestions { get; private set; } = new();

    protected FoundItem() { } 

    protected FoundItem(
        string finderId,
        DateTime foundAt,
        string location,
        List<SecurityQuestion> securityQuestions)
    {
        if (finderId == string.Empty)
            throw new ArgumentException("FinderId is required");

        if (string.IsNullOrWhiteSpace(location))
            throw new ArgumentException("Location is required");

        if (securityQuestions == null || securityQuestions.Count == 0)
            throw new ArgumentException("At least one security question is required");

        Id = Guid.NewGuid().ToString();
        FinderId = finderId;
        FoundAt = foundAt;
        Location = location;
        Status = FoundItemStatus.ReportedFound;
        SecurityQuestions = securityQuestions;
    }
}
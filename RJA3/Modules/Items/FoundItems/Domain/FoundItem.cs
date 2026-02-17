namespace RJA3.Modules.Items.FoundItems.Domain;

public abstract class FoundItem
{
    public string Id { get; private set; }
    public string FinderId { get; private set; }
    public DateTime FoundAt { get; private set; }
    public double Latitude { get; protected set; }
    public double Longitude { get; protected set; }
    public FoundItemStatus Status { get; protected set; }

    public FoundItemType ItemType { get; protected set; }

    public List<SecurityQuestion> SecurityQuestions { get; private set; } = new();

    protected FoundItem() { } 

    protected FoundItem(
        string finderId,
        DateTime foundAt,
        double latitude,
        double longitude,
        List<SecurityQuestion> securityQuestions)
    {
        Id = Guid.NewGuid().ToString();
        FinderId = finderId;
        FoundAt = foundAt;
        Latitude = latitude;
        Longitude = longitude;
        Status = FoundItemStatus.ReportedFound;
        SecurityQuestions = securityQuestions;
    }
}

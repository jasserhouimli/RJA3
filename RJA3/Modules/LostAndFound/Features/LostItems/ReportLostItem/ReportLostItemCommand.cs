using RJA3.Modules.LostAndFound.Features.LostItems.Domain;

namespace RJA3.Modules.LostAndFound.Features.LostItems.ReportLostItem
{
    public record ReportLostItemCommand
    {
        public LostItemType ItemType { get; init; }

        public Guid UserId { get; init; }

        public string Description { get; init; }

        public string LocationLost { get; init; }

        public DateTime DateLost { get; init; }

        // Phone properties

        public string? Brand { get; init; }

        public string? Model { get; init; }

        public string? Color { get; init; }


        // I will add other item specific properties later

    }

}

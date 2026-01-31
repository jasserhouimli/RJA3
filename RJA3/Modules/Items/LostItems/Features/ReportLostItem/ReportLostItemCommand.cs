using RJA3.Modules.Items.LostItems.Domain;

namespace RJA3.Modules.Items.LostItems.Features.ReportLostItem
{
    public record ReportLostItemCommand
    {
        public LostItemType ItemType { get; init; }

        public string UserId { get; init; }


        public double Latitude { get; init; }

        public double Longitude { get; init; }

       

        // Phone properties

        public string? Brand { get; init; }

        public string? Model { get; init; }

        public string? Color { get; init; }


        // I will add other item specific properties later

    }

}

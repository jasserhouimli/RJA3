using RJA3.Modules.Items.FoundItems.Domain;

namespace RJA3.Modules.Items.FoundItems.Features.ReportFoundItem
{
    public sealed class ReportFoundItemCommand
    {
        public FoundItemType ItemType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public List<SecurityQuestion> SecurityQuestions { get; set; } = new();
    }
}
using RJA3.Modules.FoundItems.Domain;

namespace RJA3.Modules.FoundItems.Features.ReportFoundItem
{
    public sealed class ReportFoundItemCommand
    {
        public FoundItemType ItemType { get; set; }
        public string LocationFound { get; set; } = default!;
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public List<SecurityQuestion> SecurityQuestions { get; set; } = new();
    }
}
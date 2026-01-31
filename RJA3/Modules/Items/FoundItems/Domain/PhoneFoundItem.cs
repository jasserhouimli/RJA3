using RJA3.Modules.Items.FoundItems;

namespace RJA3.Modules.Items.FoundItems.Domain
{
    public class PhoneFoundItem : FoundItem
    {
        public PhoneFoundItem() { }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public PhoneFoundItem(string finderId, DateTime dateFound, double latitude, double longitude ,string brand , string model , string color, List<SecurityQuestion> securityQuestions ) : base(finderId, dateFound, latitude, longitude, securityQuestions)
        {
            Brand = brand;
            Color = color;
            Model = model;
            Status = FoundItemStatus.ReportedFound;
            ItemType = FoundItemType.Phone;
        }
    }
}
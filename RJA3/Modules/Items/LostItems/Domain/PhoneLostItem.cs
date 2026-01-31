

namespace RJA3.Modules.Items.LostItems.Domain
{
    public class PhoneLostItem : LostItem
    {
        public PhoneLostItem() { }
        public string Brand { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public PhoneLostItem(string ownerId, DateTime dateLost, double latitude, double longitude, string brand , string model , string color ) : base(ownerId, dateLost, latitude, longitude)
        {
            Brand = brand;
            Color = color;
            Model = model;
            Status = LostItemStatus.ReportedLost;
            ItemType = LostItemType.Phone;
        }
    }
}

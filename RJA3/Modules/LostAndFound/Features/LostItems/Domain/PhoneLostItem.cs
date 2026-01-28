namespace RJA3.Modules.LostAndFound.Features.LostItems.Domain
{
    public class PhoneLostItem : LostItem
    {

        public string Brand { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public PhoneLostItem(Guid ownerId, DateTime dateLost, string locationLost ,string brand , string model , string color ) : base(ownerId, dateLost, locationLost)
        {
            Brand = brand;
            Color = color;
            Model = model;
            Status = LostItemStatus.Reported;
            ItemType = LostItemType.Phone;
        }
    }
}

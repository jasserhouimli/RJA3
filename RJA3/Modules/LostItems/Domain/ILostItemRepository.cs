namespace RJA3.Modules.LostItems.Domain
{
    public interface ILostItemRepository
    {
        Task AddAsync(LostItem lostItem);

        Task<List<LostItem>> GetAllLostItemsAsync();
        Task<LostItem?> GetLostItemByIdAsync(string lostItemId);
    }
}

namespace RJA3.Modules.LostItems.Domain
{
    using RJA3.Shared;

    public interface ILostItemRepository
    {
        Task AddAsync(LostItem lostItem);

        Task<List<LostItem>> GetAllLostItemsAsync();
        Task<PaginatedResult<LostItem>> GetAllLostItemsPaginatedAsync(int pageNumber, int pageSize);
        Task<LostItem?> GetLostItemByIdAsync(string lostItemId);
    }
}

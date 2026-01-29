namespace RJA3.Modules.FoundItems.Domain
{
    using RJA3.Shared;

    public interface IFoundItemRepository
    {
        Task AddAsync(FoundItem foundItem);

        Task<List<FoundItem>> GetAllFoundItemsAsync();
        Task<PaginatedResult<FoundItem>> GetAllFoundItemsPaginatedAsync(int pageNumber, int pageSize);
        Task<FoundItem?> GetFoundItemByIdAsync(string foundItemId);
    }
}
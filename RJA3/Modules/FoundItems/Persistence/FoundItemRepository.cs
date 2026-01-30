using Microsoft.EntityFrameworkCore;
using RJA3.Modules.FoundItems.Domain;
using RJA3.Shared;

namespace RJA3.Modules.FoundItems.Persistence
{
    public class FoundItemRepository : IFoundItemRepository
    {
        private readonly FoundItemDbContext _foundItemDbContext;

        public FoundItemRepository(FoundItemDbContext dbContext)
        {
            _foundItemDbContext = dbContext;
        }
        public async Task AddAsync(FoundItem foundItem)
        {
            await _foundItemDbContext.FoundItems.AddAsync(foundItem);

            await _foundItemDbContext.SaveChangesAsync();

            await Task.CompletedTask;
        }

        public async Task<FoundItem?> GetFoundItemByIdAsync(string foundItemId)
        {
            var result =  await _foundItemDbContext.FoundItems.FirstOrDefaultAsync(fi => fi.Id == foundItemId);
            return result;
        }

        public async Task<List<FoundItem>> GetAllFoundItemsAsync()
        {
            var result = await _foundItemDbContext.FoundItems.ToListAsync();
            return result;
        }

        public async Task<PaginatedResult<FoundItem>> GetAllFoundItemsPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _foundItemDbContext.FoundItems.AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResult<FoundItem>(items, totalCount, pageNumber, pageSize);
        }

        public async Task<List<FoundItem>> GetFoundItemsByTypeAsync(FoundItemType itemType)
        {
            var result = await _foundItemDbContext.FoundItems
                .Where(fi => fi.ItemType == itemType)
                .ToListAsync();
            return result;
        }


    }
}
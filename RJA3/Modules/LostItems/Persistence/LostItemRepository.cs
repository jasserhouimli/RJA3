using Microsoft.EntityFrameworkCore;
using RJA3.Modules.LostItems.Domain;
using RJA3.Shared;

namespace RJA3.Modules.LostItems.Persistence
{
    public class LostItemRepository : ILostItemRepository
    {
        private readonly LostItemDbContext _lostItemDbContext;

        public LostItemRepository(LostItemDbContext dbContext)
        {
            _lostItemDbContext = dbContext;
        }
        public async Task AddAsync(LostItem lostItem)
        {
            await _lostItemDbContext.LostItems.AddAsync(lostItem);

            await _lostItemDbContext.SaveChangesAsync();

            await Task.CompletedTask;
        }

        public async Task<LostItem?> GetLostItemByIdAsync(string lostItemId)
        {
            var result =  await _lostItemDbContext.LostItems.FirstOrDefaultAsync(li => li.Id == lostItemId);
            return result;
        }

        public async Task<List<LostItem>> GetAllLostItemsAsync()
        {
            var result = await _lostItemDbContext.LostItems.ToListAsync();
            return result;
        }

        public async Task<PaginatedResult<LostItem>> GetAllLostItemsPaginatedAsync(int pageNumber, int pageSize)
        {
            var query = _lostItemDbContext.LostItems.AsQueryable();
            var totalCount = await query.CountAsync();
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PaginatedResult<LostItem>(items, totalCount, pageNumber, pageSize);
        }


    }


}

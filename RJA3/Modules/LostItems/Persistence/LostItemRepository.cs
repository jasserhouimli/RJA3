using Microsoft.EntityFrameworkCore;
using RJA3.Modules.LostAndFound.Domain;

namespace RJA3.Modules.LostAndFound.Persistence
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

        public async Task<List<LostItem>> GetAllLostItemsAsync()
        {
            var result = await _lostItemDbContext.LostItems.ToListAsync();
            //throw new NotImplementedException();
            return result;
        }


    }


}

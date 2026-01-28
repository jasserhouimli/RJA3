using RJA3.Modules.LostAndFound.Features.LostItems.Domain;

namespace RJA3.Modules.LostAndFound.Features.LostItems.Persistence
{
    public class LostItemRepository : ILostItemRepository
    {
        public Task AddAsync(LostItem lostItem)
        {
            //throw new NotImplementedException();

            return Task.CompletedTask;
        }
    }
}

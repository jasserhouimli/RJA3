using RJA3.Modules.LostAndFound.Domain;

namespace RJA3.Modules.LostAndFound.Persistence
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

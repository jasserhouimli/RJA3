namespace RJA3.Modules.LostAndFound.Features.LostItems.Domain
{
    public interface ILostItemRepository
    {
        Task AddAsync(LostItem lostItem);

    }
}

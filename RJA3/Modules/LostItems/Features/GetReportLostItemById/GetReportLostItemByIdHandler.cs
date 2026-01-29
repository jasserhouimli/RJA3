using RJA3.Modules.LostItems.Domain;

namespace RJA3.Modules.LostItems.Features.GetReportLostItemById
{
    public sealed class GetReportLostItemByIdHandler(ILostItemRepository _lostItemRepository)
    {


        public async Task<LostItem?> Handle(GetReportLostItemByIdQuery query)
        {
            var result = await _lostItemRepository.GetLostItemByIdAsync(query.LostItemId);
            return result;
        }

        
    }
}
using FluentValidation;
using RJA3.Modules.Items.LostItems.Domain;
using RJA3.Shared;

namespace RJA3.Modules.Items.LostItems.Features.GetReportLostItemById
{
    public sealed class GetReportLostItemByIdHandler(ILostItemRepository _lostItemRepository , IValidator<GetReportLostItemByIdQuery> _validator)
    {


        public async Task<Result<LostItem>> Handle(GetReportLostItemByIdQuery query)
        {
            var validationResult = await _validator.ValidateAsync(query);
            if(!validationResult.IsValid)
            {
                return Result<LostItem>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            var result = await _lostItemRepository.GetLostItemByIdAsync(query.LostItemId);
            if (result == null)
            {
                return Result<LostItem>.Failure("Lost item not found");
            }
            return Result<LostItem>.Success(result);
        }

        
    }
}
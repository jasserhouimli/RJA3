using FluentValidation;
using RJA3.Modules.FoundItems.Domain;
using RJA3.Modules.FoundItems.Features.GetFoundItemById;
using RJA3.Modules.FoundItems.Persistence;
using RJA3.Shared;

namespace RJA3.Modules.FoundItems.Features.GetFoundItemById;

public sealed class GetFoundItemByIdHandler(IFoundItemRepository foundItemRep, IValidator<GetFoundItemByIdQuery> validator)
{
    
    public async Task<Result<FoundItem>> Handle(GetFoundItemByIdQuery query)
    {
        var validationResult = await validator.ValidateAsync(query);
        if (!validationResult.IsValid)
        {
            return Result<FoundItem>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var item = await foundItemRep.GetFoundItemByIdAsync(query.FoundItemId);
        if (item == null)
        {
            return Result<FoundItem>.Failure("Found item not found");
        }

        return Result<FoundItem>.Success(item);
    }
}
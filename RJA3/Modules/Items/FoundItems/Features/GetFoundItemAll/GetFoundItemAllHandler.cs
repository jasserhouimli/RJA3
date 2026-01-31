using FluentValidation;
using RJA3.Modules.Items.FoundItems;
using RJA3.Modules.Items.FoundItems.Domain;
using RJA3.Modules.Items.FoundItems.Persistence;
using RJA3.Shared;

namespace RJA3.Modules.Items.FoundItems.Features.GetFoundItemAll;

public sealed class GetFoundItemAllHandler(IFoundItemRepository foundItemRep, IValidator<GetFoundItemAllQuery> validator)
{
    
    public async Task<Result<PaginatedResult<FoundItem>>> Handle(GetFoundItemAllQuery query)
    {
        var validationResult = await validator.ValidateAsync(query);
        if (!validationResult.IsValid)
        {
            return Result<PaginatedResult<FoundItem>>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var items = await foundItemRep.GetAllFoundItemsPaginatedAsync(query.pageNumber, query.pageSize);

        return Result<PaginatedResult<FoundItem>>.Success(items);
    }
}
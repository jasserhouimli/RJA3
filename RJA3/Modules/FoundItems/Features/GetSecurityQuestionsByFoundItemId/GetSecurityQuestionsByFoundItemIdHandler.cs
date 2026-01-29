using FluentValidation;
using RJA3.Modules.FoundItems.Domain;
using RJA3.Modules.FoundItems.Features.GetSecurityQuestionsByFoundItemId;
using RJA3.Modules.FoundItems.Persistence;
using RJA3.Shared;

namespace RJA3.Modules.FoundItems.Features.GetSecurityQuestionsByFoundItemId;

public sealed class GetSecurityQuestionsByFoundItemIdHandler(IFoundItemRepository foundItemRep, IValidator<GetSecurityQuestionsByFoundItemIdQuery> validator)
{
    
    public async Task<Result<List<SecurityQuestion>>> Handle(GetSecurityQuestionsByFoundItemIdQuery query)
    {
        var validationResult = await validator.ValidateAsync(query);
        if (!validationResult.IsValid)
        {
            return Result<List<SecurityQuestion>>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        var item = await foundItemRep.GetFoundItemByIdAsync(query.FoundItemId);
        if (item == null)
        {
            return Result<List<SecurityQuestion>>.Failure("Found item not found");
        }

        return Result<List<SecurityQuestion>>.Success(item.SecurityQuestions);
    }
}
using FluentValidation;
using RJA3.Modules.FoundItems.Features.GetSecurityQuestionsByFoundItemId;

namespace RJA3.Modules.FoundItems.Features.GetSecurityQuestionsByFoundItemId
{
    public class GetSecurityQuestionsByFoundItemIdQueryValidator : AbstractValidator<GetSecurityQuestionsByFoundItemIdQuery>
    {
        public GetSecurityQuestionsByFoundItemIdQueryValidator()
        {
            RuleFor(x => x.FoundItemId).NotEmpty();
        }
    }
}
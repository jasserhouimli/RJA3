using FluentValidation;

namespace RJA3.Modules.Items.FoundItems.Features.GetSecurityQuestionsByFoundItemId
{
    public class GetSecurityQuestionsByFoundItemIdQueryValidator : AbstractValidator<GetSecurityQuestionsByFoundItemIdQuery>
    {
        public GetSecurityQuestionsByFoundItemIdQueryValidator()
        {
            RuleFor(x => x.FoundItemId).NotEmpty();
        }
    }
}
using FluentValidation;
using RJA3.Modules.Items.FoundItems.Features.GetFoundItemById;

namespace RJA3.Modules.Items.FoundItems.Features.GetFoundItemById
{
    public class GetFoundItemByIdQueryValidator : AbstractValidator<GetFoundItemByIdQuery>
    {
        public GetFoundItemByIdQueryValidator()
        {
            RuleFor(x => x.FoundItemId).NotEmpty();
        }
    }
}
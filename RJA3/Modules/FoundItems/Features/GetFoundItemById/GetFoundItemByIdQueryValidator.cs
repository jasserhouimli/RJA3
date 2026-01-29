using FluentValidation;
using RJA3.Modules.FoundItems.Features.GetFoundItemById;

namespace RJA3.Modules.FoundItems.Features.GetFoundItemById
{
    public class GetFoundItemByIdQueryValidator : AbstractValidator<GetFoundItemByIdQuery>
    {
        public GetFoundItemByIdQueryValidator()
        {
            RuleFor(x => x.FoundItemId).NotEmpty();
        }
    }
}
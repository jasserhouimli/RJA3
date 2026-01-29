using FluentValidation;
using RJA3.Modules.FoundItems.Features.GetFoundItemAll;

namespace RJA3.Modules.FoundItems.Features.GetFoundItemAll
{
    public class GetFoundItemAllQueryValidator : AbstractValidator<GetFoundItemAllQuery>
    {
        public GetFoundItemAllQueryValidator()
        {
            RuleFor(x => x.pageNumber).GreaterThan(0);
            RuleFor(x => x.pageSize).InclusiveBetween(1, 100);
        }
    }
}
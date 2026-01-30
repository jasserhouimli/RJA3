using FluentValidation;
using RJA3.Modules.FoundItems.Domain;
using RJA3.Modules.FoundItems.Features.ReportFoundItem;

namespace RJA3.Modules.FoundItems.Features.ReportFoundItem
{
    public class ReportFoundItemCommandValidator : AbstractValidator<ReportFoundItemCommand>
    {
        public ReportFoundItemCommandValidator()
        {
            RuleFor(x => x.ItemType).IsInEnum();
            RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");
            RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");
            RuleFor(x => x.SecurityQuestions).NotEmpty().Must(x => x.Count >= 1).WithMessage("At least one security question is required");
            // RuleForEach(x => x.SecurityQuestions).ChildRules(questions =>
            // {
            //     questions.RuleFor(q => q.Question).NotEmpty().MaximumLength(500);
            //     questions.RuleFor(q => q.ExpectedAnswer).NotEmpty().MaximumLength(500); // I will add it later 
            // });
            When(x => x.ItemType == FoundItemType.Phone, () =>
            {
                RuleFor(x => x.Brand).NotEmpty();
                RuleFor(x => x.Model).NotEmpty();
                RuleFor(x => x.Color).NotEmpty();
            });
        }
    }
}


using FluentValidation;
using RJA3.Modules.LostItems.Domain;
using RJA3.Modules.LostItems.Features.ReportLostItem;

public class ReportLostItemValidator : AbstractValidator<ReportLostItemCommand>
{
    public ReportLostItemValidator()
    {
        RuleFor(x => x.ItemType).IsInEnum().WithMessage("ItemType must be a valid enum value.");
        RuleFor(x => x.Latitude).InclusiveBetween(-90, 90).WithMessage("Latitude must be between -90 and 90.");
        RuleFor(x => x.Longitude).InclusiveBetween(-180, 180).WithMessage("Longitude must be between -180 and 180.");

        When(x => x.ItemType == LostItemType.Phone, () =>
        {
            RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand must not be empty for Phone items.");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model must not be empty for Phone items.");
            RuleFor(x => x.Color).NotEmpty().WithMessage("Color must not be empty for Phone items.");
        });
    }
}


using FluentValidation;
using RJA3.Modules.LostItems.Domain;
using RJA3.Modules.LostItems.Features.ReportLostItem;

public class ReportLostItemValidator : AbstractValidator<ReportLostItemCommand>
{
    public ReportLostItemValidator()
    {
        RuleFor(x => x.ItemType).IsInEnum().WithMessage("ItemType must be a valid enum value.");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId must not be empty.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description must not be empty.");
        RuleFor(x => x.LocationLost).NotEmpty().WithMessage("LocationLost must not be empty.");

        When(x => x.ItemType == LostItemType.Phone, () =>
        {
            RuleFor(x => x.Brand).NotEmpty().WithMessage("Brand must not be empty for Phone items.");
            RuleFor(x => x.Model).NotEmpty().WithMessage("Model must not be empty for Phone items.");
            RuleFor(x => x.Color).NotEmpty().WithMessage("Color must not be empty for Phone items.");
        });
    }
}
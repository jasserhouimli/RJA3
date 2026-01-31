using FluentValidation;

namespace RJA3.Modules.Items.LostItems.Features.GetReportLostItemById;

public class GetReportLostItemByIdValidator : AbstractValidator<GetReportLostItemByIdQuery>
{
    
    public GetReportLostItemByIdValidator()
    {
        RuleFor(x => x.LostItemId).NotEmpty().WithMessage("LostItemId must not be empty.");
    }
}
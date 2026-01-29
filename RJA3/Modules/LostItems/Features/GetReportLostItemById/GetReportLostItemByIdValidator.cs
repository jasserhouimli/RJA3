

using FluentValidation;

public class GetReportLostItemByIdValidator : AbstractValidator<GetReportLostItemByIdQuery>
{
    
    public GetReportLostItemByIdValidator()
    {
        RuleFor(x => x.LostItemId).NotEmpty().WithMessage("LostItemId must not be empty.");
    }
}
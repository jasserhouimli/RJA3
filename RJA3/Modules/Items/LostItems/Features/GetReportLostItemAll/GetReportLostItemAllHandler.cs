using FluentValidation;
using RJA3.Modules.Items.LostItems.Domain;
using RJA3.Shared;

namespace RJA3.Modules.Items.LostItems.Features.GetReportLostItemAll;

public sealed class GetReportLostItemAllHandler(
    ILostItemRepository _lostItemRepository,
    IValidator<GetReportLostItemAllQuery> _validator)
{
    public async Task<Result<PaginatedResult<LostItem>>> Handle(GetReportLostItemAllQuery query)
    {
        var validationResult = await _validator.ValidateAsync(query);
        if (!validationResult.IsValid)
        {
            return Result<PaginatedResult<LostItem>>.Failure(string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        try
        {
            var result = await _lostItemRepository.GetAllLostItemsPaginatedAsync(query.PageNumber, query.PageSize);
            return Result<PaginatedResult<LostItem>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<PaginatedResult<LostItem>>.Failure("An error occurred while retrieving lost items." + ex.Message);
        }
    }
}
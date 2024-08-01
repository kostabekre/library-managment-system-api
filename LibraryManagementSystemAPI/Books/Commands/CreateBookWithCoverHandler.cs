using FluentValidation;
using LibraryManagementSystemAPI.Books.CoverValidation;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Validators;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class CreateBookWithCoverHandler(
    IBookRepository bookRepository,
    IValidator<CoverInfo> bookCoverValidator,
    IValidator<BookCreateDto> bookValidator)
    : IRequestHandler<CreateBookWithCoverCommand, Result<int>>
{
    public async ValueTask<Result<int>> Handle(CreateBookWithCoverCommand request, CancellationToken cancellationToken)
    {
        var validationResult = bookCoverValidator.Validate(new CoverInfo(request.Dto.Cover));

        if (validationResult.IsValid == false)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }

        var createValidationResult = bookValidator.Validate(request.Dto.Details);

        if (createValidationResult.IsValid == false)
        {
            return Error.BadRequest(createValidationResult.GetErrorMessages());
        }

        return await bookRepository.CreateBookWithCoverAsync(request.Dto);
    }
}
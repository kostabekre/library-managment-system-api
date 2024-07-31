using FluentValidation;
using LibraryManagementSystemAPI.Books.CoverValidation;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class CreateBookWithCoverHandler : IRequestHandler<CreateBookWithCoverCommand, Result<int>>
{
    private readonly IBookRepository _bookRepository;
    private readonly IValidator<BookCreateDto> _bookValidator;
    private readonly IValidator<CoverInfo> _bookCoverValidator;

    public CreateBookWithCoverHandler(IBookRepository bookRepository, IValidator<CoverInfo> bookCoverValidator, IValidator<BookCreateDto> bookValidator)
    {
        _bookRepository = bookRepository;
        _bookCoverValidator = bookCoverValidator;
        _bookValidator = bookValidator;
    }

    public async ValueTask<Result<int>> Handle(CreateBookWithCoverCommand request, CancellationToken cancellationToken)
    {
        var validationResult = _bookCoverValidator.Validate(new CoverInfo(request.Dto.Cover));

        if (validationResult.IsValid == false)
        {
            return new Error(401, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        var createValidationResult = _bookValidator.Validate(request.Dto.Details);

        if (createValidationResult.IsValid == false)
        {
            return new Error(401, createValidationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        try
        {
            return await _bookRepository.CreateBookWithCoverAsync(request.Dto);
        }
        catch (Exception e)
        {
            return new Error(500, new[] { "Internal Error", e.Message });
        }
    }
}
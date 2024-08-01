using FluentValidation;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Validators;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class UpdateBookAmountHandler(IBookRepository bookRepository,
    IValidator<UpdateBookAmountCommand> validator)
    : IRequestHandler<UpdateBookAmountCommand, Error?>
{
    public async ValueTask<Error?> Handle(UpdateBookAmountCommand request, CancellationToken cancellationToken)
    {
        var validationResult =  validator.Validate(request);

        if (validationResult.IsValid == false)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }
        
        bool updated = await bookRepository.UpdateBookAmountAsync(request.Id, request.Amount);

        return updated == false ? Error.NotFound() : null;
    }
}
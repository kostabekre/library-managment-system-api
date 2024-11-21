using FluentValidation;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Validators;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

public sealed class UpdateBookNameHandler(IValidator<UpdateBookNameCommand> validator,
    IBookRepository bookRepository) : IRequestHandler<UpdateBookNameCommand, Error?>
{
    public async ValueTask<Error?> Handle(UpdateBookNameCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        
        if (validationResult.IsValid == false)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }
                
        var updated = await bookRepository.UpdateBookNameAsyns(request.Id, request.NewName);
        
        if (updated == false)
        {
            return Error.NotFound();
        }
        
        return null;
    }
}
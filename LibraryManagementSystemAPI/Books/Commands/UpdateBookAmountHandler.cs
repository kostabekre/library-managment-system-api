using LibraryManagementSystemAPI.Models;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Books.Commands;

internal sealed class UpdateBookAmountHandler : IRequestHandler<UpdateBookAmountCommand, Error?>
{
    private readonly IBookRepository _bookRepository;

    public UpdateBookAmountHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async ValueTask<Error?> Handle(UpdateBookAmountCommand request, CancellationToken cancellationToken)
    {
        bool updated = false;

        try
        {
            updated = await _bookRepository.UpdateBookAmountAsync(request.Id, request.Amount);
        }
        catch (DbUpdateException e)
        {
            return new Error(404, new[] { "Not Founded" });
        }
        catch (Exception e)
        {
            return new Error(500, new string[]{"Internal Error", e.Message});
        }

        if (updated == false)
        {
            return new Error(404, new[] { "Not Founded" });
        }

        return null;
    }
}
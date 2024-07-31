using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

public record UpdateBookAmountCommand(int Id, int Amount) : IRequest<Error?>;
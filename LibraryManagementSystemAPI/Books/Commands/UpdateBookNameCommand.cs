using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

public record UpdateBookNameCommand(string NewName, int Id) : IRequest<Error?>;
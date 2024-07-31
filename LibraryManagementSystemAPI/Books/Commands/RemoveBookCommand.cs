using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

public record RemoveBookCommand(int Id) : IRequest<Error?>;
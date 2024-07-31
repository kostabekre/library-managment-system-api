using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

public record UpdateBookRatingCommand(int Id, int Rating) : IRequest<Error?>;
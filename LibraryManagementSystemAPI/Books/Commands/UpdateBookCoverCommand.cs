using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

public record UpdateBookCoverCommand(int Id, IFormFile FormFile) : IRequest<Error?>;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

public record UpdateBookCommand(int Id, BookUpdateDto Dto) : IRequest<Error?>;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

public record CreateBookCommand(BookCreateDto BookCreateDto) : IRequest<Result<int>>;
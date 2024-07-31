using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

public record GetBookQuery(int Id) : IRequest<Result<BookInfo?>>;
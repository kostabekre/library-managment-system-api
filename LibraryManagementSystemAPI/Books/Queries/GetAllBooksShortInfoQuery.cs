using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

public record GetAllBooksShortInfoQuery() : IRequest<Result<IEnumerable<BookShortInfo>>>;
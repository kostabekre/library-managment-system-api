using LibraryManagementSystemAPI.Books.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

public record GetAllBooksShortInfoByAuthorQuery(int AuthorId) : IRequest<IEnumerable<BookShortInfo>>;
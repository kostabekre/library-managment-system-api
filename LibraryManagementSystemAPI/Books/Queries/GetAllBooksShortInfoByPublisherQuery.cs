using LibraryManagementSystemAPI.Books.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

public record GetAllBooksShortInfoByPublisherQuery(int PublisherId) : IRequest<IEnumerable<BookShortInfo>>;
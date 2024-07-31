using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

public record GetAllBooksShortInfoPageListQuery(BookParameters Parameters) : IRequest<Result<PagedList<BookShortInfo>>>;
using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Queries;

public record GetBookCoverQuery(int Id) : IRequest<Result<BookCoverDTO?>>;
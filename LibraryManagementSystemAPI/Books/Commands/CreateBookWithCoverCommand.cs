using LibraryManagementSystemAPI.Books.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Books.Commands;

public record CreateBookWithCoverCommand(BookWithCoverCreateDto Dto) : IRequest<Result<int>>;
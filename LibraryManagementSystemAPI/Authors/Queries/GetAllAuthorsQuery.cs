using LibraryManagementSystemAPI.Authors.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Queries;

public record GetAllAuthorsQuery : IRequest<IEnumerable<AuthorFullInfo>>;
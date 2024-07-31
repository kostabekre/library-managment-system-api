using LibraryManagementSystemAPI.Authors.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Queries;

public record GetAuthorQuery(int Id) : IRequest<AuthorFullInfo?>;
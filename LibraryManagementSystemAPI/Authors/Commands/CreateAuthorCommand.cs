using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Commands;

public record CreateAuthorCommand(AuthorInfo Info) : IRequest<Result<AuthorFullInfo>>;
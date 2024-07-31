using LibraryManagementSystemAPI.Authors.Models;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Commands;

public record UpdateAuthorCommand(int Id, AuthorInfo Info) : IRequest<Error?>;
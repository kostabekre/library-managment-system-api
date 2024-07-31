using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Authors.Commands;

public record DeleteAuthorCommand(int Id) : IRequest<Error?>;
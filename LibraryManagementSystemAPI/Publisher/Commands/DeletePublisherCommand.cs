using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher.Commands;

public record DeletePublisherCommand(int Id) : IRequest<Error?>;
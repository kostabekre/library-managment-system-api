using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Publisher.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher.Commands;

public record UpdatePublisherCommand(int Id, PublisherInfo Info) : IRequest<Error?>;
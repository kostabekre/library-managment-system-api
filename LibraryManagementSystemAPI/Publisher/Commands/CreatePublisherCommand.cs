using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Publisher.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher.Commands;

public record CreatePublisherCommand(PublisherInfo Info) : IRequest<Result<PublisherFullInfo>>;
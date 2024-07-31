using LibraryManagementSystemAPI.Publisher.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Publisher;

public record GetPublisherQuery(int Id) : IRequest<PublisherFullInfo?>;
using LibraryManagementSystemAPI.Genre.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Queries;

public record GetAllGenresQuery() : IRequest<IEnumerable<GenreFullInfo>>;
using LibraryManagementSystemAPI.Genre.Data;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Queries;

public record GetGenreQuery(int Id) : IRequest<GenreFullInfo?>;
using LibraryManagementSystemAPI.Genre.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Commands;

public record CreateGenreCommand(GenreInfo Info) : IRequest<Result<GenreFullInfo>>;
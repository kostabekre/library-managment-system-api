using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Commands;

public record RemoveGenreCommand(int Id) : IRequest<Error?>;
using FluentValidation;
using LibraryManagementSystemAPI.Genre.Data;
using LibraryManagementSystemAPI.Models;
using LibraryManagementSystemAPI.Validators;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Commands;

internal sealed class CreateGenreHandler(IGenreRepository genreRepository, IValidator<GenreInfo> validator)
    : IRequestHandler<CreateGenreCommand, Result<GenreFullInfo>>
{
    public async ValueTask<Result<GenreFullInfo>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request.Info, cancellationToken);
        if (validationResult.IsValid == false)
        {
            return Error.BadRequest(validationResult.GetErrorMessages());
        }
        
        var fullInfo = await genreRepository.CreateGenreAsync(request.Info);

        return fullInfo;
    }
}
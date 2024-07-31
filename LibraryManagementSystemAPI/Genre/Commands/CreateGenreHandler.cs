using FluentValidation;
using LibraryManagementSystemAPI.Genre.Data;
using LibraryManagementSystemAPI.Models;
using Mediator;

namespace LibraryManagementSystemAPI.Genre.Commands;

internal sealed class CreateGenreHandler : IRequestHandler<CreateGenreCommand, Result<GenreFullInfo>>
{
    private readonly IGenreRepository _genreRepository;
    private readonly IValidator<GenreInfo> _validator;

    public CreateGenreHandler(IGenreRepository genreRepository, IValidator<GenreInfo> validator)
    {
        _genreRepository = genreRepository;
        _validator = validator;
    }

    public async ValueTask<Result<GenreFullInfo>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request.Info);
        if (validationResult.IsValid == false)
        {
            return new Error(401, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        var fullInfo = await _genreRepository.CreateGenreAsync(request.Info);

        return fullInfo;
    }
}
using FluentValidation;
using Microsoft.Extensions.Options;

namespace LibraryManagementSystemAPI.Books.CoverValidation;

public class BookCoverValidator : AbstractValidator<CoverInfo>
{
    private static readonly string[] _fileExtensions = new string[]{".jpg", ".png"};
    private static readonly Dictionary<string, List<byte[]>> _fileSignature = 
        new Dictionary<string, List<byte[]>>
    {
        { ".jpg", new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
            }
        },
        { ".png", new List<byte[]>
            {
                new byte[] { 0x89, 0x50, 0x4E, 0x47 },
                new byte[] { 0x0D, 0x0A, 0x1A, 0x0A }
            }
        }
    };
    
    private readonly CoverValidationOptions _validationOptions;

    public BookCoverValidator(IOptions<CoverValidationOptions> validationValues)
    {
        _validationOptions = validationValues.Value;
        
        RuleFor(c => c.File)
            .Cascade(CascadeMode.Stop)
            .NotNull().WithMessage("File must not be empty")
            .Must(BeNotMoreMaxSize).WithMessage("File is more than 2 MB!")
            .Must(HaveImageExtension).WithMessage("File is jpg or png!")
            .Must(HaveValidSignature).WithMessage("File is not an image!");
    }

    private bool IsValidSignature(string extension, byte[] fileBytes)
    {
        var signatures = _fileSignature[extension];

        return signatures.Any(signature => 
            fileBytes.Take(signature.Length).SequenceEqual(signature));
    }

    private bool HaveValidSignature(IFormFile file)
    {
        var maxBytes = _fileSignature.Values.Max(l => l.Select(array => array.Length).Max());
        byte[] readenBytes = new byte[maxBytes];
        
        using (Stream openReadStream = file.OpenReadStream())
        {
            openReadStream.Read(readenBytes, 0, maxBytes);
        }
        bool isValid = false;
        foreach (var signatureKey in _fileSignature.Keys)
        {
            isValid = IsValidSignature(signatureKey, readenBytes);
            if (isValid)
            {
                break;
            }
        }

        return isValid;
    }

    private bool BeNotMoreMaxSize(IFormFile file)
    {
        return file.Length < _validationOptions.MaxSize;
    }

    private bool HaveImageExtension(IFormFile file)
    {
        var extension = Path.GetExtension(file.FileName);
        return _fileExtensions.Contains(extension);
    }
}
using Microsoft.Extensions.Options;

namespace LibraryManagementSystemAPI.CoverValidation;

public class DefaultCoverValidation : ICoverValidation
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
    
    private readonly CoverValidationValues _validationValues;

    public DefaultCoverValidation(IOptions<CoverValidationValues> validationValues)
    {
        _validationValues = validationValues.Value;
    }

    private bool IsValidSignature(string extension, byte[] fileBytes)
    {
        var signatures = _fileSignature[extension];

        return signatures.Any(signature => 
            fileBytes.Take(signature.Length).SequenceEqual(signature));
    }
    public CoverValidationResult IsFileValid(IFormFile? file)
    {
        if (file.Length > _validationValues.MaxSize)
        {
            return new CoverValidationResult() { IsValid = false, Message = "File cannot be more than 2 MB!" };
        }

        var extension = Path.GetExtension(file.FileName);
        if (!_fileExtensions.Contains(extension))
        {
            return new CoverValidationResult() { IsValid = false, Message = "File doesn't have valid extension" };
        }

        byte[] readenBytes;
        
        using (Stream openReadStream = file.OpenReadStream())
        {
            using (var memoryStream = new MemoryStream())
            {
                openReadStream.CopyTo(memoryStream);
                readenBytes = memoryStream.ToArray();
            }
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

        if (isValid == false)
        {
            return new CoverValidationResult() { IsValid = false, Message = "File is not an image!" };
        }

        return new CoverValidationResult() { IsValid = true, Message = "File is valid", Result = readenBytes, Name = $"{Guid.NewGuid()}.jpg"};
        
    }
}
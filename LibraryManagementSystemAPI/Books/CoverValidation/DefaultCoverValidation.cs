namespace LibraryManagementSystemAPI.CoverValidation;

public class DefaultCoverValidation : ICoverValidation
{
    private static readonly string[] _fileExtensions = new string[]{".jpg", ".png"};
    private static readonly Dictionary<string, List<byte[]>> _fileSignature = 
        new Dictionary<string, List<byte[]>>
    {
        { ".jpeg", new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 },
            }
        },
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
    
    private const int FileSizeBytes = 2097152;

    private bool IsValidSignature(string extension, BinaryReader reader, Stream stream)
    {
        stream.Position = 0;
        var signatures = _fileSignature[extension];
        var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

        return signatures.Any(signature => 
            headerBytes.Take(signature.Length).SequenceEqual(signature));
    }
    public CoverValidationResult IsFileValid(IFormFile? file)
    {
        if (file.Length > FileSizeBytes)
        {
            return new CoverValidationResult() { IsValid = false, Message = "File cannot be more than 2 MB!" };
        }

        var extension = Path.GetExtension(file.FileName);
        if (!_fileExtensions.Contains(extension))
        {
            return new CoverValidationResult() { IsValid = false, Message = "File is doesn't have valid extension" };
        }

        using (var openReadStream = file.OpenReadStream())
        {
            using (var reader = new BinaryReader(openReadStream))
            {
                bool isValid = false;
                isValid = IsValidSignature(".jpg", reader, openReadStream);
                if (!isValid)
                {
                    isValid = IsValidSignature(".png", reader, openReadStream);
                    if (!isValid)
                    {
                        return new CoverValidationResult() { IsValid = false, Message = "File is not an image!" };
                    }
                }

                openReadStream.Position = 0;
                using (var memoryStream = new MemoryStream())
                {
                    openReadStream.CopyTo(memoryStream);
                    var result = memoryStream.ToArray();
                    return new CoverValidationResult() { IsValid = true, Message = "File is valid", Result = result, Name = $"{Guid.NewGuid()}.jpg"};
                }
            }
        }
        
    }
}
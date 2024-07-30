namespace LibraryManagementSystemAPI.Books.CoverValidation;

public static class CoverReader
{
    public static string GetFileName()
    {
        return $"{Guid.NewGuid()}.jpg";
    }
    public static byte[] ReadAllBytes(IFormFile file)
    {
        using (var stream = file.OpenReadStream())
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
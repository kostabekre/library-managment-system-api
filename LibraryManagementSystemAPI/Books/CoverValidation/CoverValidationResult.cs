namespace LibraryManagementSystemAPI.Books.CoverValidation;

public class CoverValidationResult
{
    public bool IsValid { get; init; } 
    public byte[]? Result { get; init; }
    public string? Name { get; init; }
    public string Message { get; init; }
}
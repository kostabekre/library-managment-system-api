namespace LibraryManagementSystemAPI.Books.CoverValidation;

public interface ICoverValidation
{
    CoverValidationResult IsFileValid(IFormFile? file);
}
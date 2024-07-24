namespace LibraryManagementSystemAPI.CoverValidation;

public interface ICoverValidation
{
    CoverValidationResult IsFileValid(IFormFile? file);
}
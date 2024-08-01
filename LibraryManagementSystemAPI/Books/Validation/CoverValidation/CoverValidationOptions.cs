using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace LibraryManagementSystemAPI.Books.CoverValidation;

public class CoverValidationOptions
{
    public const string SectionName = "CoverValidation";
    
    /// <summary>
    /// Max size of a cover in bytes
    /// </summary>
    [Required]
    [Range(0, Int32.MaxValue)]
    public int MaxSize { get; init; }
}

[OptionsValidator]
public partial class CoverValidationOptionsValidator : IValidateOptions<CoverValidationOptions>{}
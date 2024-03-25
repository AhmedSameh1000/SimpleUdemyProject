using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

public class AllowedExtensionsAttributes : ValidationAttribute
{
    private readonly string _allowedExtensions;

    public AllowedExtensionsAttributes(string allowedExtensions)
    {
        _allowedExtensions = allowedExtensions;
    }

    protected override ValidationResult? IsValid(object? value,
        ValidationContext validationContext)
    {
        var File = value as IFormFile;

        if (File != null)
        {
            var Extension = Path.GetExtension(File.FileName);
            var isAllowed = _allowedExtensions.Split(",").Contains(Extension, StringComparer.OrdinalIgnoreCase);
            if (!isAllowed)
            {
                return new ValidationResult($"Only {_allowedExtensions} are Allowed");
            }
        }
        return ValidationResult.Success;
    }
}
using System.ComponentModel.DataAnnotations;

namespace CorporateCompany.ValidationAttributes;

public class AllowedExtensionsAttribute : ValidationAttribute 
{
    private readonly string[] _extensions;
    public AllowedExtensionsAttribute(string[] extensions)
    {
        _extensions = extensions;
    }
    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {

        var files = value as List<IFormFile>;
        int checkAmount = 0;
        if (files != null)
        {
            foreach (var file in files)
            {
                var fileExtension = Path.GetExtension(file.FileName);
                foreach (var extension in _extensions)
                {
                    if (fileExtension.ToLower() == extension)
                    {
                        break;
                    }
                    checkAmount++;
                }

                if (checkAmount == _extensions.Length)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
                
        }

        return ValidationResult.Success;
    }

    public string GetErrorMessage()
    {
        var allowedExtensions = string.Join(" ", _extensions);
        return $"İzin verilen görsel uzantıları: {allowedExtensions}";
    }
}
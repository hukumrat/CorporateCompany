using System.ComponentModel.DataAnnotations;

namespace CorporateCompany.ValidationAttributes;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;
    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
    {
        var files = value as List<IFormFile>;
        //var extension = Path.GetExtension(file.FileName);
        //var allowedExtensions = new[] { ".jpg", ".png" };`enter code here`

        if (files != null && files.Count > 0)
        {
            foreach (var file in files)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }
        }

        return ValidationResult.Success;
    }
    public string GetErrorMessage()
    {
        return $"İzin verilen maksimum dosya boyutu { _maxFileSize / (1024 * 1024)} Mb.";
    }
}
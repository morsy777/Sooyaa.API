public class UploadProfileImageRequestValidator : AbstractValidator<UploadProfileImageRequest>
{
    public UploadProfileImageRequestValidator()
    {
        RuleFor(x => x.Image)
            .NotEmpty()
            .WithMessage("Image file is required.");

        RuleFor(x => x.Image)
            .Must(IsValidImage)
            .WithMessage("Only JPG, JPEG, and PNG are allowed.")
            .Must(IsLessThan2MB)
            .WithMessage("Image size must be less than 2MB.")
            .When(x => x.Image is not null);
    }

    private bool IsValidImage(IFormFile? file)
    {
        if (file == null) return false;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var ext = Path.GetExtension(file.FileName).ToLower();

        return allowedExtensions.Contains(ext);
    }

    private bool IsLessThan2MB(IFormFile? file)
    {
        if (file == null) return false;

        const long maxSizeInBytes = 2 * 1024 * 1024;

        return file.Length <= maxSizeInBytes;
    }
}

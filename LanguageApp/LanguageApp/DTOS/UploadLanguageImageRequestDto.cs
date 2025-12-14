namespace LanguageApp.DTOS;

public record UploadLanguageImageRequestDto(
    int languageId,
    IFormFile Image    
);

namespace LanguageApp.Contracts.User;

public record UpdateProfileRequest(
    string FirstName,    
    string LastName
);
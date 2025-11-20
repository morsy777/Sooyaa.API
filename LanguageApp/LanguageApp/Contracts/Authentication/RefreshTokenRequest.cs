namespace LanguageApp.Contracts.Authentication;

public record RefreshTokenRequest(
    string Token,
    string RefreshToken
);

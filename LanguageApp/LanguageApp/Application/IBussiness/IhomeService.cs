using LanguageApp.DTOS;

namespace LanguageApp.Application.IBussiness
{
    public interface IhomeService
    {
        Task<homePageDTO> GetHomePageDataAsync(string userId,CancellationToken cancellationToken);
    }
}

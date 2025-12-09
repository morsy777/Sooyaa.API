using LanguageApp.DTOS;

namespace LanguageApp.Application.IBussiness
{
    public interface IWordListService
    {
        Task<IEnumerable<WordListDTO>> getLanWordListAsync(string userId,int LanId,CancellationToken cancellationToken);

        Task <string> AddNewWordListAsync(WordListRequestDTO requestDTO, CancellationToken cancellationToken);
    }
}

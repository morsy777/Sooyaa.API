using LanguageApp.DTOS;

namespace LanguageApp.Application.IBussiness
{
    public interface IWordListService
    {
        Task<IEnumerable<WordListDTO>> getAllWordListAsync(string userId,CancellationToken cancellationToken);

        Task <string> AddNewWordListAsync(WordListRequestDTO requestDTO, CancellationToken cancellationToken);
    }
}

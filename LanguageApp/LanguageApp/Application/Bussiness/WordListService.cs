using LanguageApp.Application.IBussiness;
using LanguageApp.DTOS;

namespace LanguageApp.Application.Bussiness
{
    public class WordListService: IWordListService
    {
        private readonly ApplicationDbContext _dbContext;

        public WordListService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<WordListDTO>> getAllWordListAsync(string userId, CancellationToken cancellationToken)
        {

            var words = await _dbContext.WordLists
                .Where(wl => wl.UserId == userId)
                .ToListAsync(cancellationToken);

            return words.Adapt<IEnumerable<WordListDTO>>();
        }
        public async Task<string> AddNewWordListAsync(WordListRequestDTO requestDTO, CancellationToken cancellationToken)
        {
            var isUserExist = await _dbContext.Users.
                AnyAsync(u=>u.Id == requestDTO.UserId);

            if (!isUserExist)
                return "User NotFound!";

            bool isUniqe = await _dbContext.WordLists
                .AnyAsync(wl => wl.ArabicWord == requestDTO.ArabicWord && wl.UserId == requestDTO.UserId);

            if (isUniqe) return "Word Found Before!";

            var word = requestDTO.Adapt<WordList>();
            await _dbContext.WordLists.AddAsync(word, cancellationToken);
            await _dbContext.SaveChangesAsync();
            return "Word Added Successfully !";
        }
       


    }
}

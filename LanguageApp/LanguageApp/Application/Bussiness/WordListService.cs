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


        public async Task<IEnumerable<WordListDTO>> getLanWordListAsync(string userId,int LanId ,CancellationToken cancellationToken)
        {

            var words = await _dbContext.WordLists
              .Where(wl => wl.UserId == userId && wl.LanguageId == LanId)
              .ToListAsync(cancellationToken); ;

            return words.Adapt<IEnumerable<WordListDTO>>();
        }
        public async Task<string> AddNewWordListAsync(WordListRequestDTO requestDTO, CancellationToken cancellationToken)
        {
            // Check if user exists
            var isUserExist = await _dbContext.Users
                .AnyAsync(u => u.Id == requestDTO.UserId, cancellationToken);

            if (!isUserExist)
                return "User NotFound!";

            // Check if same Arabic word already exists for this user + language
            bool isUnique = await _dbContext.WordLists
                .AnyAsync(wl =>
                    wl.ArabicWord == requestDTO.ArabicWord &&
                    wl.UserId == requestDTO.UserId &&
                    wl.LanguageId == requestDTO.LanguageId,    
                    cancellationToken
                );

            if (isUnique)
                return "Word Found Before!";

            // Map DTO → Entity
            var word = requestDTO.Adapt<WordList>();

            await _dbContext.WordLists.AddAsync(word, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return "Word Added Successfully!";
        }



    }
}

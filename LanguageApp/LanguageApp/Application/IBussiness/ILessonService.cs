using LanguageApp.DTOS;

namespace LanguageApp.Application.IBussiness
{
    public interface ILessonService
    {
        Task<IEnumerable<LessonDTO>> GetAllLessonsForLanguageForCategoryAsync(int languageId, int categoryId, CancellationToken cancellationToken);
        Task<string> AddLessonToSavedLessonsAsync(SaveLessonRequestDTO dTO, CancellationToken cancellationToken);
        Task<string>RemoveLessonFromSavedLessonsAsync(SaveLessonRequestDTO dTO, CancellationToken cancellationToken);
        Task <IEnumerable<SavedLessonResponseDTO>> GetSavedLessonsForUserForLanguageAsync(string userId, int languageId,CancellationToken cancellationToken);
        Task<LessonDTO> GetLessonByIdAsync(int lessonId, CancellationToken cancellationToken);

    }
}

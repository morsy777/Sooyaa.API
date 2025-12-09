using LanguageApp.DTOS;

namespace LanguageApp.Dashboard.Interface
{
    public interface IDashboardService
    {

      //================== Language Management =================//
     
      // Get all languages
      Task<IEnumerable<LanguagesDTO>> GetAllLanguagesAsync(CancellationToken cancellationToken);
     
      // Add a new language
      Task<string> AddLanguageAsync(string language, CancellationToken cancellationToken);
     
      // Update language
      Task<LanguagesDTO> UpdateLanguageAsync(int languageId, string language, CancellationToken cancellationToken);
     
      // Delete language
      Task<bool> DeleteLanguageAsync(int languageId, CancellationToken cancellationToken);
     
     
      //================== Level Management =================//
     
      // Get all levels
      Task<IEnumerable<LevelDTO>> GetAllLevelsAsync(CancellationToken cancellationToken);

        // Get All Levels by Language Id
        Task<IEnumerable<LevelDTO>> GetLevelsByLanguageIdAsync(int languageId, CancellationToken cancellationToken);
        // Add new level
        Task<LevelDTO> AddLevelAsync(LevelDTORequest level, CancellationToken cancellationToken);
     
      // Update level
      Task<LevelDTO> UpdateLevelAsync(int levelId, LevelDTORequest level, CancellationToken cancellationToken);
     
      // Delete level
      Task<bool> DeleteLevelAsync(int levelId, CancellationToken cancellationToken);
          
      
     
     
      //================== Chapters Management =================//

        // get All Chapters
        Task<IEnumerable<ChapterDTO>> GetAllChaptersAsync(CancellationToken cancellationToken);
        // get All Chapters by Level Id
        Task<IEnumerable<ChapterDTO>> GetChaptersByLevelIdAsync(int levelId, CancellationToken cancellationToken);

        //Add New Chapter
        Task<ChapterDTO> AddChapterAsync(ChapterDTORequest chapter, CancellationToken cancellationToken);
        //Update Chapter
        Task<ChapterDTO> UpdateChapterAsync(int chapterId, ChapterDTORequest chapter, CancellationToken cancellationToken);
        //Delete Chapter
        Task<bool> DeleteChapterAsync(int chapterId, CancellationToken cancellationToken);

        //================== Lessons Management =================//
        // get All Lessons
        Task<IEnumerable<LessonDTO>> GetAllLessonsAsync(CancellationToken cancellationToken);
        // get All Lessons by Chapter Id
        Task<IEnumerable<LessonDTO>> GetLessonsByChapterIdAsync(int chapterId, CancellationToken cancellationToken);

        //Add New Lesson
        Task<LessonDTO> AddLessonAsync(LessonDTORequest lesson, CancellationToken cancellationToken);
        //Update Lesson
        Task<LessonDTO> UpdateLessonAsync(int lessonId, LessonDTORequest lesson, CancellationToken cancellationToken);
        //Delete Lesson
        Task<bool> DeleteLessonAsync(int lessonId, CancellationToken cancellationToken);

        //================== Questions Management =================//
        // get All Questions
        Task<IEnumerable<QuestionDTO>> GetAllQuetionsAsync(CancellationToken cancellationToken);
        // get All Questions by Lesson Id
        Task<IEnumerable<QuestionDTO>> GetQuestionsByLessonIdAsync(int lessonId, CancellationToken cancellationToken);

        //Add New Question
        Task<QuestionDTO> AddQuestionAsync(QuestionDTORequest question, CancellationToken cancellationToken);
        //Update Question
        Task<QuestionDTO> UpdateQuestionAsync(int questionId,QuestionDTORequest question, CancellationToken cancellationToken);
        //Delete Question
        Task<bool> DeleteQuestionAsync(int questionId, CancellationToken cancellationToken);

        //================== Answers Management =================//
        // get All Answers
        Task<IEnumerable<AnswerDTO>> GetAllAnswersAsync(CancellationToken cancellationToken);
        // get All Answers by Question Id
        Task<IEnumerable<AnswerDTO>> GetAnswersByQuestionIdAsync(int questionId, CancellationToken cancellationToken);

        //Add New Answer
        Task<AnswerDTO> AddAnswerAsync(AnswerDTORequest answer, CancellationToken cancellationToken);
        //Update Answer
        Task<AnswerDTO> UpdateAnswerAsync(int answerId, AnswerDTORequest answer, CancellationToken cancellationToken);
        //Delete Answer
        Task<bool> DeleteAnswerAsync(int answerId, CancellationToken cancellationToken);


        //================== Category Management =================//
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync(CancellationToken cancellationToken);
    }
}

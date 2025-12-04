using LanguageApp.Dashboard.Interface;
using LanguageApp.DTOS;
using LanguageApp.Entities;

namespace LanguageApp.Dashboard.Service
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _dbContext;

        public DashboardService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //================== Language Management =================//
        public async Task<IEnumerable<LanguagesDTO>> GetAllLanguagesAsync(CancellationToken cancellationToken)
        {
            var languages = await _dbContext.Languages
                .ToListAsync(cancellationToken);

            return languages.Adapt<IEnumerable<LanguagesDTO>>();
        }
        public async Task<string> AddLanguageAsync(string language, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(language))
                return "Language name cannot be empty.";

            language = language.Trim();

            bool exists = await _dbContext.Languages
            .AnyAsync(x => x.Name.ToLower() == language.ToLower(), cancellationToken);


            if (exists)
                return "This language already exists.";

            var lan = new Language { Name = language };
            
            await _dbContext.Languages.AddAsync(lan, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return "Created Successfully";
        }
        public async Task<LanguagesDTO> UpdateLanguageAsync(int lanId, string language, CancellationToken cancellationToken)
        {
            var lan = await _dbContext.Languages
                .FirstOrDefaultAsync(x => x.Id == lanId, cancellationToken);

            if (lan == null)
                return null!; 

            lan.Name = language.Trim();

            await _dbContext.SaveChangesAsync(cancellationToken);

            return lan.Adapt<LanguagesDTO>();
        }
        public async Task<bool> DeleteLanguageAsync(int lanId, CancellationToken cancellationToken)
        {
            var lan = await _dbContext.Languages.FindAsync(lanId, cancellationToken);

            if (lan == null)
                return false;

            _dbContext.Languages.Remove(lan);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }


        //================== Level Management =================//
        public async Task<IEnumerable<LevelDTO>> GetAllLevelsAsync(CancellationToken cancellationToken)
        {
            var levels = await _dbContext.Levels
                .ToListAsync(cancellationToken);

            return levels.Adapt<IEnumerable<LevelDTO>>();
        }
        public async Task<IEnumerable<LevelDTO>> GetLevelsByLanguageIdAsync(int languageId, CancellationToken cancellationToken)
        {
            var levels = await _dbContext.Levels
                .Where(l => l.LanguageId == languageId)
                .ToListAsync(cancellationToken);

            return levels.Adapt<IEnumerable<LevelDTO>>();
        }
        public async Task<LevelDTO> AddLevelAsync(LevelDTORequest level, CancellationToken cancellationToken)
        {
            if (level == null || string.IsNullOrWhiteSpace(level.Name) || level.LanId <= 0)
                return null!; // invalid input

            var language = await _dbContext.Languages
                .FirstOrDefaultAsync(l => l.Id == level.LanId, cancellationToken);

            if (language == null)
                return null!; // language not found

            var levelName = level.Name.Trim();

            bool exists = await _dbContext.Levels
                .AnyAsync(l => l.Name.ToLower() == levelName.ToLower() && l.LanguageId == level.LanId, cancellationToken);

            if (exists)
                return null!; // level already exists for this language

            var lev = level.Adapt<Level>();
            lev.Name = levelName;
            lev.LanguageId = level.LanId;

            await _dbContext.Levels.AddAsync(lev, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var resultDto = lev.Adapt<LevelDTO>();
            resultDto.LanId = lev.LanguageId;

            return resultDto;
        }
        public async Task<LevelDTO> UpdateLevelAsync(int levelId, LevelDTORequest dto, CancellationToken cancellationToken)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name) || dto.LanId <= 0)
                return null!; // Invalid input

            // Get the existing level
            var lev = await _dbContext.Levels
                .FirstOrDefaultAsync(x => x.Id == levelId, cancellationToken);

            if (lev == null)
                return null!; // Level not found

            var newName = dto.Name.Trim();

            // Check for duplicates in the same language (excluding current level)
            bool exists = await _dbContext.Levels
                .AnyAsync(x => x.Id != levelId
                               && x.LanguageId == dto.LanId
                               && x.Name.ToLower() == newName.ToLower(),
                               cancellationToken);

            if (exists)
                return null!; // Duplicate found

            // Update properties
            dto.Adapt(lev);
            lev.Name = newName;
            lev.LanguageId = dto.LanId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return lev.Adapt<LevelDTO>();
        }
        public async Task<bool> DeleteLevelAsync(int levelId, CancellationToken cancellationToken)
        {
            var lev = await _dbContext.Levels
            .FindAsync(levelId , cancellationToken);

            if (lev == null)
                return false;

            _dbContext.Levels.Remove(lev);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        //================== Chapters Management =================//
        public async Task<IEnumerable<ChapterDTO>> GetAllChaptersAsync(CancellationToken cancellationToken)
        {
            var chapters = await _dbContext.Chapters
                .ToListAsync(cancellationToken);

            return chapters.Adapt<IEnumerable<ChapterDTO>>();
        }
        public async Task<IEnumerable<ChapterDTO>> GetChaptersByLevelIdAsync(int levelId, CancellationToken cancellationToken)
        {
            var chapters = await _dbContext.Chapters
                 .Where(c => c.LevelId == levelId)
                 .ToListAsync(cancellationToken);

            return chapters.Adapt<IEnumerable<ChapterDTO>>();
        }
        public async Task<ChapterDTO> AddChapterAsync(ChapterDTORequest chapter, CancellationToken cancellationToken)
        {
            if (chapter == null ||string.IsNullOrWhiteSpace(chapter.Name) || chapter.LevelId <= 0 || chapter.OrderNumber<=0)
                return null!; // invalid input

            // Validate Level exists
            var level = await _dbContext.Levels
                .FirstOrDefaultAsync(l => l.Id == chapter.LevelId, cancellationToken);

            if (level == null)
                return null!;

            var chapterName = chapter.Name.Trim();

            // Check duplicate chapter in same level
            bool exists = await _dbContext.Chapters
                .AnyAsync(c =>
                    c.Name.ToLower() == chapterName.ToLower() &&
                    c.LevelId == chapter.LevelId,
                    cancellationToken);

            if (exists)
                return null!; // duplicate found

            var chap = chapter.Adapt<Chapter>();
            chap.Name = chapterName;
            chap.CreatedAt = DateTime.UtcNow;

            await _dbContext.Chapters.AddAsync(chap, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return chap.Adapt<ChapterDTO>();
        }
        public async Task<ChapterDTO> UpdateChapterAsync(int chapterId, ChapterDTORequest dto, CancellationToken cancellationToken)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name) || dto.LevelId <= 0 || dto.OrderNumber<=0)
                return null!; // Invalid input

            // Find the Chapter
            var chapter = await _dbContext.Chapters
                .FirstOrDefaultAsync(x => x.Id == chapterId, cancellationToken);

            if (chapter == null)
                return null!; // Not found

            var newName = dto.Name.Trim();

            // Check for duplicate name in the same Level (excluding itself)
            bool exists = await _dbContext.Chapters
                .AnyAsync(x => x.Id != chapterId &&
                               x.LevelId == dto.LevelId &&
                               x.Name.ToLower() == newName.ToLower(),
                           cancellationToken);

            if (exists)
                return null!; // Duplicate chapter name for same level

           
            dto.Adapt(chapter);

            chapter.Name = newName;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return chapter.Adapt<ChapterDTO>();
        }
        public async Task<bool> DeleteChapterAsync(int chapterId, CancellationToken cancellationToken)
        {
            var chap = await _dbContext.Chapters
                       .FindAsync(chapterId, cancellationToken);

            if (chap == null)
                return false;

            _dbContext.Chapters.Remove(chap);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        //================== Lessons Management =================//
        public async Task<IEnumerable<LessonDTO>> GetAllLessonsAsync(CancellationToken cancellationToken)
        {
            var lessons = await _dbContext.Lessons
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return lessons.Adapt<IEnumerable<LessonDTO>>();
        }
        public async Task<IEnumerable<LessonDTO>> GetLessonsByChapterIdAsync(int chapterId, CancellationToken cancellationToken)
        {
            var lessons = await _dbContext.Lessons
                    .Where(l => l.ChapterId == chapterId)
                    .ToListAsync(cancellationToken);

            return lessons.Adapt<IEnumerable<LessonDTO>>();
        }
        public async Task<LessonDTO> AddLessonAsync(LessonDTORequest lesson, CancellationToken cancellationToken)
        {
            if (lesson == null || string.IsNullOrWhiteSpace(lesson.Title) || lesson.ChapterId <= 0 || lesson.OrderNumber<=0)
                return null!; // invalid input

            var chapter = await _dbContext.Chapters
                .FirstOrDefaultAsync(c => c.Id == lesson.ChapterId, cancellationToken);

            if (chapter == null)
                return null!; // chapter not found

            var lessonName = lesson.Title.Trim();

            bool exists = await _dbContext.Lessons
                .AnyAsync(l => l.Title.ToLower() == lessonName.ToLower()
                               && l.ChapterId == lesson.ChapterId,
                          cancellationToken);

            if (exists)
                return null!; // duplicate lesson inside same chapter

            var ent = lesson.Adapt<Lesson>();
            ent.Title = lessonName;
            ent.ChapterId = lesson.ChapterId;

            await _dbContext.Lessons.AddAsync(ent, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return ent.Adapt<LessonDTO>();
        }
        public async Task<LessonDTO> UpdateLessonAsync(int lessonId, LessonDTORequest dto, CancellationToken cancellationToken)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Title) || dto.ChapterId <= 0 || dto.OrderNumber<=0)
                return null!; // invalid input

            var lesson = await _dbContext.Lessons
                .FirstOrDefaultAsync(l => l.Id == lessonId, cancellationToken);

            if (lesson == null)
                return null!; // not found

            var newName = dto.Title.Trim();

            bool exists = await _dbContext.Lessons
                .AnyAsync(x => x.Id != lessonId
                               && x.ChapterId == dto.ChapterId
                               && x.Title.ToLower() == newName.ToLower(),
                          cancellationToken);

            if (exists)
                return null!; // duplicate name inside same chapter

            dto.Adapt(lesson);
            lesson.Title = newName;
            lesson.ChapterId = dto.ChapterId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return lesson.Adapt<LessonDTO>();
        }
        public async Task<bool> DeleteLessonAsync(int lessonId, CancellationToken cancellationToken)
        {
            var lesson = await _dbContext.Lessons
                .FirstOrDefaultAsync(l => l.Id == lessonId, cancellationToken);

            if (lesson == null)
                return false;

            _dbContext.Lessons.Remove(lesson);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        //================== Question Management =================//
        public async Task<IEnumerable<QuestionDTO>> GetAllQuetionsAsync(CancellationToken cancellationToken)
        {
            var questions = await _dbContext.Questions
                   .AsNoTracking()
                   .ProjectToType<QuestionDTO>()
                   .ToListAsync(cancellationToken);

            return questions;
        }
        public async Task<IEnumerable<QuestionDTO>> GetQuestionsByLessonIdAsync(int lessonId, CancellationToken cancellationToken)
        {
            var questions = await _dbContext.Questions
                 .Where(q => q.LessonId == lessonId)
                 .ToListAsync(cancellationToken);

            return questions.Adapt<IEnumerable<QuestionDTO>>();
        }
        public async Task<QuestionDTO> AddQuestionAsync(QuestionDTORequest question, CancellationToken cancellationToken)
        {
            if (question == null || string.IsNullOrWhiteSpace(question.QuestionText) || question.LessonId <= 0)
                return null!; // invalid Input

            var lesson = await _dbContext.Lessons
             .FirstOrDefaultAsync(l => l.Id == question.LessonId, cancellationToken);

            if (lesson == null) 
                return null!; // Lesson not Found

            string qText = question.QuestionText.Trim();

            bool exists = await _dbContext.Questions
                .AnyAsync(x => x.QuestionText.ToLower() == qText.ToLower()
                               && x.LessonId == question.LessonId,
                          cancellationToken);

            if (exists)
                return null!; // Question Already Exist

            var q = question.Adapt<Question>();
            q.QuestionText = qText;

            await _dbContext.Questions.AddAsync(q, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return q.Adapt<QuestionDTO>();

        }
        public async Task<QuestionDTO> UpdateQuestionAsync(int questionId,QuestionDTORequest question, CancellationToken cancellationToken)
        {
            if (question == null || string.IsNullOrWhiteSpace(question.QuestionText) || question.LessonId <= 0)
                return null!; // Invalid input

            var q = await _dbContext.Questions
            .FirstOrDefaultAsync(x => x.Id == questionId, cancellationToken);

            if (q == null)
                return null!; //Not Found

            string newText = question.QuestionText.Trim();

            bool exists = await _dbContext.Questions
                .AnyAsync(x => x.Id != questionId &&
                               x.LessonId == question.LessonId &&
                               x.QuestionText.ToLower() == newText.ToLower(),
                          cancellationToken);

            if (exists)
                return null!;

            question.Adapt(q);
            q.QuestionText = newText;
            q.LessonId = question.LessonId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return q.Adapt<QuestionDTO>();

        }
        public async Task<bool> DeleteQuestionAsync(int questionId, CancellationToken cancellationToken)
        {
            var q = await _dbContext.Questions
                 .FirstOrDefaultAsync(x => x.Id == questionId, cancellationToken);

            if (q == null)
                return false;

            _dbContext.Questions.Remove(q);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        //================== Answer Management =================//
        public async Task<IEnumerable<AnswerDTO>> GetAllAnswersAsync(CancellationToken cancellationToken)
        {
            var answers = await _dbContext.Answers
                    .AsNoTracking()
                    .ProjectToType<AnswerDTO>()
                    .ToListAsync(cancellationToken);

            return answers;
        }
        public async Task<IEnumerable<AnswerDTO>> GetAnswersByQuestionIdAsync(int questionId, CancellationToken cancellationToken)
        {
            var answers = await _dbContext.Answers
                 .Where(a => a.QuestionId == questionId)
                 .ToListAsync(cancellationToken);

            return answers.Adapt<IEnumerable<AnswerDTO>>();
        }
        public async Task<AnswerDTO> AddAnswerAsync(AnswerDTORequest answer, CancellationToken cancellationToken)
        {
            if (answer == null || string.IsNullOrWhiteSpace(answer.AnswerText) || answer.QuestionId <= 0)
                return null!;

            var question = await _dbContext.Questions
                .FirstOrDefaultAsync(q => q.Id == answer.QuestionId, cancellationToken);

            if (question == null)
                return null!;

            string answerText = answer.AnswerText.Trim();

            bool exists = await _dbContext.Answers
                .AnyAsync(x => x.AnswerText.ToLower() == answerText.ToLower()
                               && x.QuestionId == answer.QuestionId,
                          cancellationToken);

            if (exists)
                return null!;

            var ans = answer.Adapt<Answer>();
            ans.AnswerText = answerText;

            await _dbContext.Answers.AddAsync(ans, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return ans.Adapt<AnswerDTO>();
        }
        public async Task<AnswerDTO> UpdateAnswerAsync(int answerId, AnswerDTORequest dto, CancellationToken cancellationToken)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.AnswerText) || dto.QuestionId <= 0)
                return null!;

            var ans = await _dbContext.Answers
                .FirstOrDefaultAsync(a => a.Id == answerId, cancellationToken);

            if (ans == null)
                return null!;

            string newText = dto.AnswerText.Trim();

            bool exists = await _dbContext.Answers
                .AnyAsync(a => a.Id != answerId &&
                               a.QuestionId == dto.QuestionId &&
                               a.AnswerText.ToLower() == newText.ToLower(),
                          cancellationToken);

            if (exists)
                return null!;

            dto.Adapt(ans);
            ans.AnswerText = newText;
            ans.QuestionId = dto.QuestionId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return ans.Adapt<AnswerDTO>();
        }
        public async Task<bool> DeleteAnswerAsync(int answerId, CancellationToken cancellationToken)
        {
            var ans = await _dbContext.Answers
                .FirstOrDefaultAsync(a => a.Id == answerId, cancellationToken);

            if (ans == null)
                return false;

            _dbContext.Answers.Remove(ans);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }







    }
}

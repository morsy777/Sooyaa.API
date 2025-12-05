using LanguageApp.Abstractions;
using LanguageApp.Dashboard.Interface;
using LanguageApp.DTOS;
using LanguageApp.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LanguageApp.Dashboard.Controller
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        private readonly IAdminService _adminService;

        public DashboardController(IDashboardService dashboardService, IAdminService adminService)
        {
            _dashboardService = dashboardService;
            _adminService = adminService;
        }

        // ======================================================
        //                  LANGUAGES ENDPOINTS
        // ======================================================

        #region Get all languages
        [HttpGet("getLanguages")]
        [ProducesResponseType(typeof(IEnumerable<LanguagesDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllLanguages(CancellationToken cancellationToken)
        {
            var languages = await _dashboardService.GetAllLanguagesAsync(cancellationToken);
            return Ok(languages);
        }
        #endregion

        #region Add language
        [HttpPost("addLanguages")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddLanguage([FromBody] string language, CancellationToken cancellationToken)
        {
            if (language.IsNullOrEmpty())
                return BadRequest(new { Message = "Invalid input" });

            var createdLanguage = await _dashboardService.AddLanguageAsync(language, cancellationToken);

            if (createdLanguage == "This language already exists.")
                return BadRequest(new { Message = createdLanguage });

            return Created(string.Empty, new { Message = createdLanguage });

        }
        #endregion

        #region Update language
        [HttpPut("updateLanguage/{languageId:int}")]
        [ProducesResponseType(typeof(LanguagesDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLanguage(int languageId, [FromBody] string language, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(language))
                return BadRequest(new { Message = "Language cannot be empty" });

            var updated = await _dashboardService.UpdateLanguageAsync(languageId, language, cancellationToken);

            if (updated == null)
                return NotFound(new { Message = "Language not found" });

            return Ok(updated);
        }

        #endregion

        #region Delete language
        [HttpDelete("deleteLanguages/{languageId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLanguage(int languageId, CancellationToken cancellationToken)
        {
            var deleted = await _dashboardService.DeleteLanguageAsync(languageId, cancellationToken);

            if (!deleted)
                return NotFound(new { Message = "Language not found" });

            return Ok(new { Deleted = true });
        }

        #endregion

        // ======================================================
        //                  LEVELS ENDPOINTS
        // ======================================================

        #region Get all levels
        [HttpGet("getLevels")]
        [ProducesResponseType(typeof(IEnumerable<LevelDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllLevels(CancellationToken cancellationToken)
        {
            var levels = await _dashboardService.GetAllLevelsAsync(cancellationToken);
            return Ok(levels);
        }
        #endregion

        #region Get levels by language id
        [HttpGet("getLevelByLanguageId/{languageId}")]
        [ProducesResponseType(typeof(IEnumerable<LevelDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetLevelsByLanguageId(int languageId, CancellationToken cancellationToken)
        {
            if (languageId <= 0)
                return BadRequest(new { Message = "Invalid language ID." });

            var levels = await _dashboardService.GetLevelsByLanguageIdAsync(languageId, cancellationToken);
            return Ok(levels);
        }
        #endregion

        #region Add level
        [HttpPost("addLevel")]
        [ProducesResponseType(typeof(LevelDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddLevel([FromBody] LevelDTORequest level, CancellationToken cancellationToken)
        {
           
            var created = await _dashboardService.AddLevelAsync(level, cancellationToken);

            if (created == null)
                return BadRequest(new { Message = "Level already exists or invalid input." });

            return Created(string.Empty, created);
        }
        #endregion

        #region Update level
        [HttpPut("updateLevels/{levelId:int}")]
        [ProducesResponseType(typeof(LevelDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLevel(int levelId, [FromBody] LevelDTORequest level, CancellationToken cancellationToken)
        {
            var updated = await _dashboardService.UpdateLevelAsync(levelId, level, cancellationToken);

            if (updated == null)
                return BadRequest(new { Message = "Invalid input, level not found, or duplicate name." });

            return Ok(updated);
        }
        #endregion

        #region Delete level
        [HttpDelete("deleteLevels/{levelId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLevel(int levelId, CancellationToken cancellationToken)
        {
            var deleted = await _dashboardService.DeleteLevelAsync(levelId, cancellationToken);

            if (!deleted)
                return NotFound(new { Message = "Level not found" });

            return Ok(new { Deleted = true });
        }
        #endregion

        // ======================================================
        //                  CHAPTERS ENDPOINTS
        // ======================================================

        #region Get all chapters
        [HttpGet("getChapters")]
        [ProducesResponseType(typeof(IEnumerable<ChapterDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllChapters(CancellationToken cancellationToken)
        {
            var chapters = await _dashboardService.GetAllChaptersAsync(cancellationToken);
            return Ok(chapters);
        }
        #endregion

        #region Get Chapter by Level id
        [HttpGet("getChapterByLevelId/{LevelId}")]
        [ProducesResponseType(typeof(IEnumerable<ChapterDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetchaptersByLevelId(int LevelId, CancellationToken cancellationToken)
        {
            if (LevelId <= 0)
                return BadRequest(new { Message = "Invalid Level Id." });

            var chapters = await _dashboardService.GetChaptersByLevelIdAsync(LevelId, cancellationToken);
            return Ok(chapters);
        }
        #endregion

        #region Add chapters
        [HttpPost("addChapter")]
        [ProducesResponseType(typeof(ChapterDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddChapter([FromBody] ChapterDTORequest chapter, CancellationToken cancellationToken)
        {
            var created = await _dashboardService.AddChapterAsync(chapter, cancellationToken);

            if (created == null)
                return BadRequest(new { Message = "Invalid input, level not found, or duplicate chapter." });

            return StatusCode(StatusCodes.Status201Created, created);
        }
        #endregion

        #region Update chapter
        [HttpPut("updateChapter/{chapterId:int}")]
        [ProducesResponseType(typeof(ChapterDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateChapter(int chapterId, [FromBody] ChapterDTORequest chapter, CancellationToken cancellationToken)
        {
            var updated = await _dashboardService.UpdateChapterAsync(chapterId, chapter, cancellationToken);

            if (updated == null)
                return BadRequest(new { Message = "Invalid input, level not found, or duplicate Chapter name." });

            return Ok(updated);
        }
        #endregion

        #region Delete chapter
        [HttpDelete("deleteChapter/{chapterId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteChapter(int chapterId, CancellationToken cancellationToken)
        {
            var deleted = await _dashboardService.DeleteChapterAsync(chapterId, cancellationToken);

            if (!deleted)
                return NotFound(new { Message = "Chapter not found" });

            return Ok(new { Deleted = true });
        }
        #endregion

        // ======================================================
        //                  LESSONS ENDPOINTS
        // ======================================================
        #region Get all lessons
        [HttpGet("getLessons")]
        [ProducesResponseType(typeof(IEnumerable<LessonDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllLessons(CancellationToken cancellationToken)
        {
            var Lessons = await _dashboardService.GetAllLessonsAsync(cancellationToken);
            return Ok(Lessons);
        }
        #endregion

        #region Get Lesson by chapter id
        [HttpGet("getLessonBychapterId/{chapterId}")]
        [ProducesResponseType(typeof(IEnumerable<LessonDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getLessonBychapterId(int chapterId, CancellationToken cancellationToken)
        {
            if (chapterId <= 0)
                return BadRequest(new { Message = "Invalid chapterID." });

            var Lessons = await _dashboardService.GetLessonsByChapterIdAsync(chapterId, cancellationToken);
            return Ok(Lessons);
        }
        #endregion

        #region Add lessons
        [HttpPost("addLesson")]
        [ProducesResponseType(typeof(LessonDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddLesson([FromBody] LessonDTORequest lesson, CancellationToken cancellationToken)
        {
            var created = await _dashboardService.AddLessonAsync(lesson, cancellationToken);

            if (created == null)
                return BadRequest(new { Message = "Invalid input, Chapter not found, or duplicate Lesson." });

            return StatusCode(StatusCodes.Status201Created, created);
        }
        #endregion

        #region Update lesson
        [HttpPut("updateLesson/{lessonId:int}")]
        [ProducesResponseType(typeof(LessonDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLesson(int lessonId, [FromBody] LessonDTORequest lesson, CancellationToken cancellationToken)
        {
            var updated = await _dashboardService.UpdateLessonAsync(lessonId, lesson, cancellationToken);

            if (updated == null)
                return BadRequest(new { Message = "Invalid input, chapter not found, or duplicate Lesson name." });

            return Ok(updated);
        }
        #endregion

        #region Delete lesson
        [HttpDelete("deleteLesson/{lessonId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLesson(int lessonId, CancellationToken cancellationToken)
        {
            var deleted = await _dashboardService.DeleteLessonAsync(lessonId, cancellationToken);

            if (!deleted)
                return NotFound(new { Message = "Lesson not found" });

            return Ok(new { Deleted = true });
        }

        #endregion

        // ======================================================
        //                  Questions ENDPOINTS
        // ======================================================

        #region Get all Questions
        [HttpGet("getQuestion")]
        [ProducesResponseType(typeof(IEnumerable<QuestionDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllQuestions(CancellationToken cancellationToken)
        {
            var Questions = await _dashboardService.GetAllQuetionsAsync(cancellationToken);
            return Ok(Questions);
        }
        #endregion

        #region Get question by Lesson id
        [HttpGet("getQuestionByLessonId/{lessonId}")]
        [ProducesResponseType(typeof(IEnumerable<QuestionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getQuestionByLessonId(int lessonId, CancellationToken cancellationToken)
        {
            if (lessonId <= 0)
                return BadRequest(new { Message = "Invalid lessonID." });

            var questions = await _dashboardService.GetQuestionsByLessonIdAsync(lessonId, cancellationToken);
            return Ok(questions);
        }
        #endregion

        #region Add Questions
        [HttpPost("addQuestion")]
        [ProducesResponseType(typeof(QuestionDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddQuestion([FromBody] QuestionDTORequest question, CancellationToken cancellationToken)
        {
            var created = await _dashboardService.AddQuestionAsync(question, cancellationToken);

            if (created == null)
                return BadRequest(new { Message = "Invalid input, Lesson not found, or duplicate Question." });

            return StatusCode(StatusCodes.Status201Created, created);
        }
        #endregion

        #region Update Question
        [HttpPut("updateQuestion/{questionId:int}")]
        [ProducesResponseType(typeof(QuestionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateQuestion(int questionId, [FromBody] QuestionDTORequest question, CancellationToken cancellationToken)
        {
            var updated = await _dashboardService.UpdateQuestionAsync(questionId, question, cancellationToken);

            if (updated == null)
                return BadRequest(new { Message = "Invalid input, Lesson not found, or duplicate Question ." });

            return Ok(updated);
        }
        #endregion

        #region Delete Question
        [HttpDelete("deleteQuestion/{questionId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteQuestion(int questionId, CancellationToken cancellationToken)
        {
            var deleted = await _dashboardService.DeleteQuestionAsync(questionId, cancellationToken);

            if (!deleted)
                return NotFound(new { Message = "Question not found" });

            return Ok(new { Deleted = true });
        }
        #endregion

        // ======================================================
        //                  Answers ENDPOINTS
        // ======================================================
        #region Get all Answers
        [HttpGet("getAnswers")]
        [ProducesResponseType(typeof(IEnumerable<AnswerDTO>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAnswers(CancellationToken cancellationToken)
        {
            var Answers = await _dashboardService.GetAllAnswersAsync(cancellationToken);
            return Ok(Answers);
        }
        #endregion

        #region Get Answers by Question id
        [HttpGet("getAnswersByQuestionId/{questionId}")]
        [ProducesResponseType(typeof(IEnumerable<AnswerDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> getAnswersByQuestionId(int questionId, CancellationToken cancellationToken)
        {
            if (questionId <= 0)
                return BadRequest(new { Message = "Invalid questionID." });

            var Answers = await _dashboardService.GetAnswersByQuestionIdAsync(questionId, cancellationToken);
            return Ok(Answers);
        }
        #endregion

        #region Add Answers
        [HttpPost("addAnswer")]
        [ProducesResponseType(typeof(AnswerDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAnswer([FromBody] AnswerDTORequest answer, CancellationToken cancellationToken)
        {
            var created = await _dashboardService.AddAnswerAsync(answer, cancellationToken);

            if (created == null)
                return BadRequest(new { Message = "Invalid input, Question not found, or duplicate Answer." });

            return StatusCode(StatusCodes.Status201Created, created);
        }
        #endregion

        #region Update Answer
        [HttpPut("updateAnswer/{answerId:int}")]
        [ProducesResponseType(typeof(AnswerDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAnswer(int answerId, [FromBody] AnswerDTORequest answer, CancellationToken cancellationToken)
        {
            var updated = await _dashboardService.UpdateAnswerAsync(answerId, answer, cancellationToken);

            if (updated == null)
                return BadRequest(new { Message = "Invalid input, Question not found, or duplicate Answer ." });

            return Ok(updated);
        }
        #endregion

        #region Delete Answer
        [HttpDelete("deleteAnswer/{answerId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAnswer(int answerId, CancellationToken cancellationToken)
        {
            var deleted = await _dashboardService.DeleteAnswerAsync(answerId, cancellationToken);

            if (!deleted)
                return NotFound(new { Message = "Answer not found" });

            return Ok(new { Deleted = true });
        }
        #endregion

        // ======================================================
        //                  Admin ENDPOINTS
        // ======================================================

        [HttpPut("prompote-to-admin/{userId}")]
        public async Task<IActionResult> PrompoteToAdmin([FromBody] string userId)
        {
            var isAdded = await _adminService.PromoteToAdminAsync(userId);

            return isAdded ? NoContent() : BadRequest("Invalid User Id");
        }

    }
}

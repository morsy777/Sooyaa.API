using LanguageApp.Application.IBussiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        #region Get All Lessons for language for category
        [HttpGet("GetLessons/language/{languageId}/category/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllLessonsForUserForLanguageForCategory( int languageId, int categoryId,CancellationToken cancellationToken)
        {
            if (languageId <= 0 || categoryId <= 0)
            {
                return BadRequest(new { Message = "Invalid languageId or categoryId" });
            }
            var lessons = await _lessonService.GetAllLessonsForLanguageForCategoryAsync(languageId, categoryId,cancellationToken);
            return Ok(lessons);
        }
        #endregion

        #region Add Lesson to saved lessons
        [HttpPost("SaveLesson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddLessonToSavedLessons(SaveLessonRequestDTO dTO,CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(dTO.userId) ||dTO.lessonId <= 0)
            {
                return BadRequest(new { Message = "Invalid userId or lessonId" });
            }
            var result = await _lessonService.AddLessonToSavedLessonsAsync(dTO,cancellationToken);
            return Ok(result);
        }
        #endregion

        #region get saved lessons for user for language
        [HttpGet("GetSavedLessons/user/{userId}/language/{languageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetSavedLessonsForUserForLanguage(string userId, int languageId,CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(userId) || languageId <= 0)
            {
                return BadRequest(new { Message = "Invalid userId or languageId" });
            }
            var lessons = await _lessonService.GetSavedLessonsForUserForLanguageAsync(userId, languageId,cancellationToken);
            return Ok(lessons);
        }
        #endregion


    }
}

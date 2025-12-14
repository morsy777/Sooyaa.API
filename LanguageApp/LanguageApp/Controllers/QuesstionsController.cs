using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuesstionsController : ControllerBase
    {
        private readonly IQuestionService _questionService;

        public QuesstionsController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        #region Get Questions for a Lesson
        [HttpGet("getQuesttions/{lessonId}")]
        public async Task<IActionResult> GetQuestionsForLesson(int lessonId,CancellationToken cancellationToken)
        {
            if (lessonId <= 0)
                return BadRequest("Invalid lesson id");

            var questions = await _questionService.GetQuestionsByLessonAsync(lessonId, cancellationToken);

            if (questions == null || !questions.Any())
                return NotFound("No questions found for this lesson");

            return Ok(questions);
        }
        #endregion

    }
}

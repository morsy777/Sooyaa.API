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

        #region Get All Lessons by CategoryId and Chapter
        [HttpGet("getAllLessons/category/{categoryId}/chapter/{chapterId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllLessons(int categoryId, int chapterId,CancellationToken cancellationToken)
        {
            if(categoryId <= 0 || chapterId <= 0)
            {
                return BadRequest(new { Message ="Invalid categoryId or chapterId" });
            }
            var lessons = await _lessonService.GetAllLessonsAync(categoryId, chapterId,cancellationToken);
            return Ok(lessons);
        }

        #endregion


    }
}

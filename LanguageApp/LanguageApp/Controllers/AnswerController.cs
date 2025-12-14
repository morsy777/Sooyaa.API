using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswersService _answersService;

        public AnswerController(IAnswersService answersService)
        {
            _answersService = answersService;
        }

        #region check-answer
        [HttpPost("check-answer")]
        public async Task<IActionResult> CheckAnswer([FromBody] CheckAnswerDTO dto, CancellationToken cancellationToken)
        {
            if (dto.QuestionId <= 0 || dto.SelectedOptionId <= 0)
                return BadRequest("Invalid question or answer ID");

            var result = await _answersService.CheckAnswerAsync(dto.QuestionId,dto.SelectedOptionId,cancellationToken);

            return Ok(result);
        }
        #endregion


    }
}

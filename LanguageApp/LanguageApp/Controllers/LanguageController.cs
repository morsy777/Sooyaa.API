using FluentValidation.Resources;
using LanguageApp.Application.IBussiness;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        #region Get All Languages
        [HttpGet("getAllLanguages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllLanguages()
        {
            var languages = await _languageService.GetAllLanguagesAsync();
            if (languages == null || !languages.Any())
            {
                return NotFound(new { Message = "No Language Found !" });
            }
            return Ok(languages);
        }
        #endregion

        [HttpPost("upload-lang-image")]
        public async Task<IActionResult> UploadLanguageImage([FromForm] UploadLanguageImageRequestDto request)
        {
            await _languageService.UploadLanguageImageAsync(request);
            return Created();
        }

        [HttpGet("get-lang-image/{languageId}")]
        public async Task<IActionResult> GetLanguageImage([FromRoute] int languageId)
        {
            var result = await _languageService.GetLanguageImageAsync(languageId, Request);

            if (result == null)
                return BadRequest("There is no Image");

            return Ok(result);
        }
    }
}

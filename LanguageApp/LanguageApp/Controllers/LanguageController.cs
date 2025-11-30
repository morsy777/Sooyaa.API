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
                return NotFound(new {Message ="No Language Found !"});
            }
            return Ok(languages);
        }
        #endregion


    }
}

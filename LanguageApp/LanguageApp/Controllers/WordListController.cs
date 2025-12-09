using LanguageApp.Application.IBussiness;
using LanguageApp.DTOS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordListController : ControllerBase
    {
        private readonly IWordListService _wordListService;

        public WordListController(IWordListService wordListService)
        {
            _wordListService = wordListService;
        }

        #region Get WordList for a User for a Language

        [HttpGet("getLanWordList/{userId}/{LanId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllWordLists(string userId,int LanId,CancellationToken cancellationToken)
        {
            var wordLists = await _wordListService.getLanWordListAsync(userId,LanId,cancellationToken);
            return Ok(wordLists);
        }
        #endregion

        #region Add New Word List
        [HttpPost("AddNewWordList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddWordList(WordListRequestDTO requestDTO,CancellationToken cancellationToken)
        {
            var result = await _wordListService.AddNewWordListAsync(requestDTO, cancellationToken);

            return Ok(new { Message = result });
        }
        #endregion



    }
}
